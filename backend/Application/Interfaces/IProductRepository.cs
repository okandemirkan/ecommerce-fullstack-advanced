using Application.Enums;
using Application.Pagination;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetAllProductsPaged(ProductSortType sortType,int pageNumber, int pageSize);
        Task<PagedList<Product>> GetProductsByCategoryId(ProductSortType sortType, int categoryId, int pageNumber, 
            int pageSize);
        Task<Product?> GetProductById(int id);
        Task<PagedList<Product>> GetSoftDeletedProducts(int pageNumber, int pageSize);
        Task<Product?> GetSoftDeletedProductById(int productId);
        Task<Product?> GetAnyProductById(int productId);
        Task<PagedList<Product>> SearchProductsByName(ProductSortType sortType,string productName, int pageNumber, int pageSize);
        Task<PagedList<Product>> SearchProductsByNameAsAdmin(ProductSortType sortType,string productName, int pageNumber, int pageSize);
        Task AddProduct(Product newProduct);
        Task UpdateProduct(Product updatedProduct);
        Task AddStock(int id, int quantity);
        Task DeleteProduct(Product product);
        Task<bool> IsProductExist(int productId);

    }
}
