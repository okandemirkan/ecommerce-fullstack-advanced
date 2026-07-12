import api from "./api";

export const getCurrentUserCart = async () => {
    const response = await api.get("/CartItem/Get-Current-User-Items");
    return response.data.response;
}

export const addCartItem = async (productId, quantity = 1) => {
    const response = await api.post("/CartItem/Add-Cart-Item", { productId, quantity });
    return response.data.response;
}

export const updateCartItemQuantity = async (cartItemId, quantity) => {
    const response = await api.patch(`/CartItem/Update-Item-Quantity?cartItemId=${cartItemId}&quantity=${quantity}`);
    return response.data.response;
}

export const removeCartItem = async (cartItemId) => {
    const response = await api.delete(`/CartItem/Delete-Current-User-Item/${cartItemId}`);
    return response.data.response;
}

export const clearCart = async () => {
    const response = await api.delete("/CartItem/Clear-Current-User-CartItems");
    return response.data.response;
}