
using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<PagedList<Category>> GetAllCategoriesPaged(int pageNumber, int pageSize);
        Task<Category> GetCategoryById(int categoryId);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
    }
}
