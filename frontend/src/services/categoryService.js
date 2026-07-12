import api from "./api";

export const getAllCategories = async (pageNumber = 1, pageSize = 100) => {
  const response = await api.get("/Category/Get-All-Categories", {
    params: { pageNumber, pageSize },
  });
  return response.data.response;
};

export const addCategory = async (categoryName) => {
  const response = await api.post("/Category/Add-Category", null, {
    params: { categoryName },
  });
  return response.data.response;
};

export const updateCategory = async (categoryId, categoryName) => {
  const response = await api.patch("/Category/Update-Category-Name", {
    categoryId,
    categoryName,
  });
  return response.data.response;
};

export const deleteCategory = async (categoryId) => {
  const response = await api.delete("/Category/Delete-Category", {
    params: { categoryId },
  });
  return response.data.response;
};
