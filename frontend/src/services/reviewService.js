import api from "./api";

export const getReviewsByProductId = async (productId) => {
    const response = await api.get(`/Review/Get-Reviews-By-ProductId/${productId}`);
    return response.data.response;
}

export const getReviewsByUserId = async (userId) => {
    const response = await api.get(`/Review/Get-Reviews-By-UserId/${userId}`);
    return response.data.response;
}

export const getCurrentUserReviews = async () => {
    const response = await api.get("/Review/Get-Reviews");
    return response.data.response;
}

export const addReview = async (review) => {
    const response = await api.post("/Review/Add-Review", review);
    return response.data.response;
}

export const updateReview = async (reviewId, review) => {
    const response = await api.put(`/Review/Update-Review/${reviewId}`, review);
    return response.data.response;
}

export const deleteReview = async (reviewId) => {
    const response = await api.delete(`/Review/Delete-Review?reviewId=${reviewId}`);
    return response.data.response;
}

export const deleteReviewAsAdmin = async (reviewId, userId) => {
    const response = await api.delete(`/Review/Delete-Review-As-Admin?reviewId=${reviewId}&userId=${userId}`);
    return response.data.response;
}
