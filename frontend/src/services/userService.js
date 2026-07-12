import api from "./api";

export const getProfile = async () => {
    const response = await api.get("/User/Get-Current-User");
    return response.data.response;
}

export const isCurrentUserActive = async () => {
    const response = await api.get("/User/Is-User-Active");
    return response.data.response;
}

export const updateProfile = async (data) => {
    const response = await api.put("/User/Update-Current-User",data);
    return response.data.response;
}

export const changePassword = async (data) => {
    const response = await api.patch("/User/Change-User-Password",data);
    return response.data.response;
}

export const getAddresses = async () => {
    const response = await api.get("/Address/Get-Current-User-Addresses");
    return response.data.response;
}

export const addAddress = async (data) => {
    const response = await api.post("/Address/Add-Address-To-Current-User",data);
    return response.data.response;
}

export const updateAddress = async (addressId, data) => {
    const response = await api.put(`/Address/Update-Current-User-Address?AddressId=${addressId}`, data);
    return response.data.response;
}

export const deleteAddress = async (addressId) => {
    const response = await api.delete(`/Address/Delete-Current-User-Address?AddressId=${addressId}`);
    return response.data.response;
}

// Kullanıcı kendi hesabını kalıcı olarak siler (email + şifre doğrulaması frontend'de yapılır)
export const deleteCurrentUser = async () => {
    const response = await api.delete("/User/Delete-Current-User");
    return response.data.response;
}
