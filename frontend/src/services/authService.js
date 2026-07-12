import api from "./api";
const roleClaim= "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
export const AUTH_CHANGED_EVENT = "authChanged";

function emitAuthChanged() {
    window.dispatchEvent(new Event(AUTH_CHANGED_EVENT));
}

function decodeTokenPayload(token) {
    try {
        const payload = token.split(".")[1];
        if (!payload) return null;

        const normalizedPayload = payload
            .replace(/-/g, "+")
            .replace(/_/g, "/")
            .padEnd(Math.ceil(payload.length / 4) * 4, "=");

        return JSON.parse(atob(normalizedPayload));
    } catch {
        return null;
    }
}

export const setAuthToken = (token) => {
    if (token) localStorage.setItem("token", token);
    else localStorage.removeItem("token");
    emitAuthChanged();
}

export const register = async (formData) => {
    const response = await api.post(`Auth/Register`, { user: formData });
    return response.data;
}

export const login = async (formData) => {
    const response = await api.post(`/Auth/Login`, {
        email: formData.eMail, 
        password: formData.password
    });
    setAuthToken(response.data.response);
    return response.data;
}

export const logout = () => {
    setAuthToken(null);
}

export const isLoggedIn = () => { //kullanıcı giriş yapmış mı?
    const token = localStorage.getItem("token");
    if (!token) return false;

    const payload = decodeTokenPayload(token);
    if (!payload) return false;
    return !payload.exp || payload.exp * 1000 > Date.now();
}

export const getRole = () => {
    const token = localStorage.getItem("token");
    if(!token) return null;
    const payload = decodeTokenPayload(token);
    return payload?.[roleClaim] ?? null;
    //index'e 1 verme sebebimiz : 0:header, 1:payload, 2:signature. 
    //atob ile base64'ü stringe; jsonparse ile string'i json'a çeviriyoruz.
}
