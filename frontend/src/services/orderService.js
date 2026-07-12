import api from "./api";

export const getCurrentUserOrders = async(pageNumber = 1, pageSize = 10) =>{
    const response = await api.get("/Order/Get-Current-User-Orders", {
        params: { pageNumber, pageSize }
    });
    return response.data.response;
}

export const updateOrderStatus = async(orderId, status) => {
    const response = await api.patch(`/Order/Update-Order-Status/${orderId}`, null, {
        params: { status }
    });
    return response.data.response;
}

export const cancelOrder = async(orderId) => {
    const response = await api.patch("/Order/Cancel-Order", null, {
        params: { orderId }
    });
    return response.data.response;
}

export const addOrderToCurrentUser = async(order) => {
    const response = await api.post("/Order/Add-Order-To-Current-User", order);
    return response.data.response;
}

export const addOrderItemToCurrentUser = async(dto) => {
    const response = await api.post("/Order/Add-Order-Item-To-Current-User", dto);
    return response.data.response;
}

export const removeOrderItem = async(dto) => {
    const response = await api.delete("/Order/Remove-Order-Item", { data: dto });
    return response.data.response;
}

export const updateShippingAddress = async(orderId, addressId) => {
    const response = await api.patch("/Order/Update-Shipping-Address", { OrderId: orderId, AddressId: addressId });
    return response.data.response;
}
