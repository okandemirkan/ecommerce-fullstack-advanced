import api from "./api";

export const getAllProducts = async (pageNumber = 1, pageSize = 12, sortType = "Normal") => {
    const response = await api.get("Product/Get-All-Products", {
        params: { sortType, pageNumber, pageSize }
    });
    return response.data.response;
}

export const searchProductsByName = async (productName, pageNumber = 1, pageSize = 12, sortType = "Normal") => {
    const response = await api.get("/Product/Search-Products-By-Name", {
        params: { productName, sortType, pageNumber, pageSize }
    });
    return response.data.response;
}

export const getProductsByCategoryId = async (
    categoryId,
    pageNumber = 1,
    pageSize = 12,
    sortType = "Normal"
) => {
    const response = await api.get(`/Product/Get-Products-By-Category/${categoryId}`, {
        params: { sortType, pageNumber, pageSize }
    });
    return response.data.response;
}

export const getProductById = async (productId) => {
    const response = await api.get(`/Product/Get-Product-By-Id/${productId}`);
    return response.data.response;
}
