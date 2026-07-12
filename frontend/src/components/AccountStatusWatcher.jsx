import { useCallback, useEffect, useRef, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { AUTH_CHANGED_EVENT, isLoggedIn, logout } from "../services/authService";
import { isCurrentUserActive } from "../services/userService";
import "./AccountStatusWatcher.css";

const ACTIVE_CHECK_INTERVAL_MS = 5000;
const LOGIN_REDIRECT_DELAY_MS = 1800;
const STATUS_RETRY_DELAY_MS = 60000;
const ACCOUNT_SUSPENDED_EVENT = "accountSuspended";

function AccountStatusWatcher() {
  const navigate = useNavigate();
  const location = useLocation();
  const [authVersion, setAuthVersion] = useState(0);
  const [isSuspended, setIsSuspended] = useState(false);
  const checkingRef = useRef(false);
  const redirectTimerRef = useRef(null);
  const nextStatusRetryAtRef = useRef(0);

  const suspendSession = useCallback(() => {
    if (redirectTimerRef.current) return;

    setIsSuspended(true);
    logout();
    redirectTimerRef.current = window.setTimeout(() => {
      navigate("/login", { replace: true, state: { accountSuspended: true } });
    }, LOGIN_REDIRECT_DELAY_MS);
  }, [navigate]);

  const checkAccountStatus = useCallback(async () => {
    if (checkingRef.current || isSuspended || !isLoggedIn()) return;
    if (Date.now() < nextStatusRetryAtRef.current) return;

    checkingRef.current = true;
    try {
      const isActive = await isCurrentUserActive();
      if (isActive === false) suspendSession();
    } catch (error) {
      if (error?.response?.status === 401) return;
      if (!error?.response || error.response.status >= 500) {
        nextStatusRetryAtRef.current = Date.now() + STATUS_RETRY_DELAY_MS;
      }
    } finally {
      checkingRef.current = false;
    }
  }, [isSuspended, suspendSession]);

  useEffect(() => {
    const handleAuthChanged = () => setAuthVersion((current) => current + 1);

    window.addEventListener(AUTH_CHANGED_EVENT, handleAuthChanged);
    window.addEventListener("storage", handleAuthChanged);
    window.addEventListener(ACCOUNT_SUSPENDED_EVENT, suspendSession);

    return () => {
      window.removeEventListener(AUTH_CHANGED_EVENT, handleAuthChanged);
      window.removeEventListener("storage", handleAuthChanged);
      window.removeEventListener(ACCOUNT_SUSPENDED_EVENT, suspendSession);
    };
  }, [suspendSession]);

  useEffect(() => {
    if (isSuspended || !isLoggedIn()) return;

    checkAccountStatus();
    const intervalId = window.setInterval(checkAccountStatus, ACTIVE_CHECK_INTERVAL_MS);
    const handleFocus = () => checkAccountStatus();
    const handleVisibilityChange = () => {
      if (!document.hidden) checkAccountStatus();
    };

    window.addEventListener("focus", handleFocus);
    document.addEventListener("visibilitychange", handleVisibilityChange);

    return () => {
      window.clearInterval(intervalId);
      window.removeEventListener("focus", handleFocus);
      document.removeEventListener("visibilitychange", handleVisibilityChange);
    };
  }, [authVersion, checkAccountStatus, isSuspended, location.pathname]);

  useEffect(() => {
    return () => {
      if (redirectTimerRef.current) window.clearTimeout(redirectTimerRef.current);
    };
  }, []);

  if (!isSuspended) return null;

  return (
    <div className="account-status-overlay" role="alert" aria-live="assertive">
      <div className="account-status-dialog">
        <h2>Hesabınız askıya alınmıştır</h2>
        <p>Oturumunuz sonlandırılıyor. Lütfen tekrar giriş yapın.</p>
      </div>
    </div>
  );
}

export default AccountStatusWatcher;
