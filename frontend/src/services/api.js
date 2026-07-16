import axios from "axios";
import { notify } from "../utils/notify";

const apiBaseUrl = import.meta.env.VITE_API_URL?.trim() || "/api";

const api = axios.create({
  baseURL: apiBaseUrl,
  timeout: 30000,
});

const AUTH_ENDPOINTS = ["/auth/login", "/auth/register"];
const ACCOUNT_STATUS_ENDPOINT = "/user/is-user-active";
const ACCOUNT_SUSPENDED_EVENT = "accountSuspended";
const PUBLIC_ENDPOINTS = ["/category/", "/product/"];
const ACCOUNT_STATUS_RETRY_DELAY_MS = 60000;
let sessionRedirectTimer = null;
let accountStatusCheckPromise = null;
let accountStatusRetryUntil = 0;

function isAuthEndpoint(url = "") {
  const normalizedUrl = url.toLowerCase();
  return AUTH_ENDPOINTS.some(endpoint => normalizedUrl.includes(endpoint));
}

function isAccountStatusEndpoint(url = "") {
  return url.toLowerCase().includes(ACCOUNT_STATUS_ENDPOINT);
}

function isPublicEndpoint(url = "") {
  const normalizedUrl = url.toLowerCase();
  return PUBLIC_ENDPOINTS.some(endpoint => normalizedUrl.includes(endpoint));
}

async function checkCurrentUserIsActive(token) {
  if (Date.now() < accountStatusRetryUntil) return true;

  if (!accountStatusCheckPromise) {
    accountStatusCheckPromise = axios
      .get("/User/Is-User-Active", {
        baseURL: api.defaults.baseURL,
        headers: { Authorization: `Bearer ${token}` },
      })
      .then(response => response.data?.response !== false)
      .catch(error => {
        if (!error?.response || error.response.status >= 500) {
          accountStatusRetryUntil = Date.now() + ACCOUNT_STATUS_RETRY_DELAY_MS;
          return true;
        }
        throw error;
      })
      .finally(() => {
        accountStatusCheckPromise = null;
      });
  }

  return accountStatusCheckPromise;
}

api.interceptors.request.use(async config => {
  const token = localStorage.getItem("token");    //Her requestte token bilgilerini gönderiyor.
  if (token && !isAuthEndpoint(config.url)) {
    if (!isAccountStatusEndpoint(config.url) && !isPublicEndpoint(config.url)) {
      const isActive = await checkCurrentUserIsActive(token).catch(() => true);
      if (!isActive) {
        window.dispatchEvent(new Event(ACCOUNT_SUSPENDED_EVENT));
        localStorage.removeItem("token");
        window.dispatchEvent(new Event("authChanged"));
        const error = new Error("Account suspended");
        error.code = "ACCOUNT_SUSPENDED";
        return Promise.reject(error);
      }
    }
    config.headers.Authorization = `Bearer ${token}`;
  }
  config.headers["Content-Type"] = "application/json";
  return config;
});

api.interceptors.response.use(
  response => {
    if (isAuthEndpoint(response.config?.url) && sessionRedirectTimer) {
      clearTimeout(sessionRedirectTimer);
      sessionRedirectTimer = null;
    }
    return response;
  },
  error => {
    if (error.response?.status === 401 && !isAuthEndpoint(error.config?.url)) {
      const currentToken = localStorage.getItem("token");
      const authorizationHeader = error.config?.headers?.Authorization;
      const failedRequestToken = typeof authorizationHeader === "string"
        ? authorizationHeader.replace(/^Bearer\s+/i, "")
        : null;

      // Eski oturumdan geç dönen bir 401, yeni giriş yapan kullanıcının token'ını silemez.
      if (!failedRequestToken || failedRequestToken === currentToken) {
        localStorage.removeItem("token");
        window.dispatchEvent(new Event("authChanged"));
        notify.warning("Oturumunuzun süresi doldu, lütfen tekrar giriş yapın.");

        if (sessionRedirectTimer) clearTimeout(sessionRedirectTimer);
        sessionRedirectTimer = setTimeout(() => {
          if (!localStorage.getItem("token")) window.location.href = "/login";
          sessionRedirectTimer = null;
        }, 1500);
      }
    }
    return Promise.reject(error);
  }
);

export default api;
