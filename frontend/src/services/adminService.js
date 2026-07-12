import api from "./api";

//Address

export const getAddressesByUserId = async(userId) =>{
    const response = await api.get(`/Address/Get-Addresses-By-UserId/${userId}`)
    return response.data.response;
}

export const addAddressByUserId = async(userId,address) =>{
    const response = await api.post(`/Address/Add-Address-By-UserId/${userId}`,address);
    return response.data.response;
}

export const updateAddressByUserId = async(userId,addressId,address) => {
    const response = await api.put(
        `/Address/Update-Address-By-AddressId?userId=${userId}&addressId=${addressId}`,
        address
    );
    return response.data.response;
}

export const deleteAddressByAddressId = async(userId,addressId) => {
    const response = await api.delete(`/Address/Delete-Address?userId=${userId}&addressId=${addressId}`);
    return response.data.response;
}

//Order 

export const getOrdersByUserId = async(userId, pageNumber = 1, pageSize = 10) =>{
    const response = await api.get(`/Order/Get-Order-By-UserId/${userId}`, {
        params: { pageNumber, pageSize }
    });
    return response.data.response;
}

export const getLastOrders = async(pageNumber = 1, pageSize = 5) =>{
    const response = await api.get(`/Order/Get-Last-Orders`, { params: { pageNumber, pageSize } });
    return response.data.response;
}

export const addOrder = async(order) => {
    const response = await api.post(`/Order/Add-Order`, {
        userId: order.userId,
        addOrderDto: {
            productId: order.productId,
            addressId: order.addressId,
            quantity: order.quantity
        }
    });
    return response.data.response;
}

export const updateOrderStatus = async(orderId, status) =>{
    const response = await api.patch(`/Order/Update-Order-Status/${orderId}`, null, {
        params: { status }
    });
    return response.data.response;
}

//Product

export const addProduct = async(product) =>{
    const response = await api.post(`/Product/Add-Product`,product)
    return response.data.response;
}

export const updateProduct = async(productId,product) =>{
    const response = await api.put(`/Product/Update-Product/${productId}`,product);
    return response.data.response;
}

// Soft delete (pasife al) - eski deleteProduct artık bunu çağırıyor
export const softDeleteProduct = async(productId)=>{
    const response = await api.delete(`/Product/Soft-Delete-Product/${productId}`);
    return response.data.response;
}

// Hard delete (kalıcı sil)
export const hardDeleteProduct = async(productId)=>{
    const response = await api.delete(`/Product/Delete-Product?productId=${productId}`);
    return response.data.response;
}

export const updateProductImage = async(productId, imageUrl)=>{
    const response = await api.patch(`/Product/Update-Product-Image`, null, {
        params: { id: productId, imageUrl }
    });
    return response.data.response;
}

export const removeProductImage = async(productId)=>{
    const response = await api.patch(`/Product/Remove-Product-Image/${productId}`);
    return response.data.response;
}

export const addStock = async(productId,quantity)=>{
    const response = await api.patch(`/Product/Add-Stock/${productId}?quantity=${quantity}`);
    return response.data.response;
}

export const getSoftDeletedProducts = async(pageNumber = 1, pageSize = 10) => {
    const response = await api.get(`/Product/Get-All-Soft-Deleted-Products`, { params: { pageNumber, pageSize } });
    return response.data.response;
}

export const getSoftDeletedProductById = async(productId) => {
    const response = await api.get(`/Product/Get-Soft-Deleted-Product-By-Id/${productId}`);
    return response.data.response;
}

export const restoreProduct = async(productId) => {
    const response = await api.patch(`/Product/Restore-Product/${productId}`);
    return response.data.response;
}

export const searchProductsByNameAsAdmin = async(productName, pageNumber = 1, pageSize = 10, sortType = "Normal") => {
    const response = await api.get(`/Product/Search-Products-By-Name-As-Admin`, { params: { productName, sortType, pageNumber, pageSize } });
    return response.data.response;
}

//User

export const getAllUsers = async (pageNumber = 1, pageSize = 10) => {
    const response = await api.get(`/User/Get-All-Users`, { params: { pageNumber, pageSize } });
    return response.data.response; // { items, pageNumber, pageSize, totalCount, totalPages, ... }
}

export const getSoftDeletedUsers = async (pageNumber = 1, pageSize = 10) => {
    const response = await api.get(`/User/Get-Soft-Deleted-Users`, { params: { pageNumber, pageSize } });
    return response.data.response;
}

export const getUserById = async(userId)=>{
    const response = await api.get(`/User/Get-User-By-Id`, { params: { userId } });
    return response.data.response;
}

export const getUserByMail = async(mail)=>{
    const response = await api.get(`/User/Get-User-By-Mail`, { params: { mail } });
    return response.data.response;
}

export const getUserByPhoneNumber = async(phoneNumber)=>{
    const response = await api.get(`/User/Get-User-By-Phone-Number`, { params: { phoneNumber } });
    return response.data.response;
}

export const searchUsersByName = async(userName, pageNumber = 1, pageSize = 10)=>{
    const response = await api.get(`/User/Search-Users-By-Name`, { params: { userName, pageNumber, pageSize } });
    return response.data.response;
}

export const getSoftDeletedUserById = async(userId)=>{
    const response = await api.get(`/User/Get-Soft-Deleted-User-By-Id`, { params: { userId } });
    return response.data.response;
}

export const updateUser = async(userId,userInfo)=>{
    const response = await api.put(`/User/Update-User/${userId}`,userInfo);
    return response.data.response;
}

export const makeAdmin = async(userId)=>{
    const response = await api.patch(`/User/Make-User-Admin/${userId}`);
    return response.data.response;
}

export const removeAdminRole = async(userId)=>{
    const response = await api.patch(`/User/Remove-Admin-Role?id=${userId}`);
    return response.data.response;
}

// Soft delete - kullanıcıyı pasife al
export const softDeleteUser = async(userId)=>{
    const response = await api.delete(`/User/Soft-Delete-User?userId=${userId}`);
    return response.data.response;
}

// Hard delete - kalıcı sil
export const hardDeleteUser = async(userId)=>{
     const response = await api.delete(`/User/Delete-User?userId=${userId}`);
     return response.data.response;
}

// Pasif kullanıcıyı geri getir
export const restoreUser = async(userId)=>{
    const response = await api.patch(`/User/Restore-Deleted-User?userId=${userId}`);
    return response.data.response;
}
