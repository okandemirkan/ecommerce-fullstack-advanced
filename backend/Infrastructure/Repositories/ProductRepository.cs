using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Pagination;
using Application.Enums;
namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<Product>> GetAllProductsPaged(ProductSortType sortType, int pageNumber, int pageSize)
        {

            var query = _context.Products.Include(p => p.Category).AsQueryable();
            var totalCount = await query.CountAsync();
            query = ApplySorting(query, sortType);

            var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<Product>(products, totalCount, pageNumber, pageSize);
        }

        public async Task<PagedList<Product>> GetProductsByCategoryId(ProductSortType sortType, int categoryId, int pageNumber,
            int pageSize)
        {
            var query = _context.Products.Include(p => p.Category).Where(p => p.CategoryId == categoryId);

            var totalCount = await query.CountAsync();
            query = ApplySorting(query, sortType);

            var productsPaged = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).
                ToListAsync(); ;

            return new PagedList<Product>(productsPaged, totalCount, pageNumber, pageSize);

        }
        public async Task<PagedList<Product>> GetSoftDeletedProducts(int pageNumber, int pageSize)
        {
            var query = _context.Products.IgnoreQueryFilters().Include(p => p.Category).Where(p =>
                p.IsDeleted && p.WorkspaceId == _context.CurrentWorkspaceId);

            var totalCount = await query.CountAsync();

            var products = await query.OrderBy(product => product.Id)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return new PagedList<Product>(products, totalCount, pageNumber, pageSize);
        }

        public async Task<Product?> GetSoftDeletedProductById(int productId)
        {
            var product = await _context.Products.Where(p => p.IsDeleted == true).Include(p=>p.Category)
                .IgnoreQueryFilters().FirstOrDefaultAsync(p=>p.Id == productId &&
                    p.WorkspaceId == _context.CurrentWorkspaceId);
            return product;
        }
        public async Task<Product?> GetProductById(int id)
        {
            var product = await _context.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }
        public async Task<Product?> GetAnyProductById(int productId)
        {
            var product = await _context.Products.IgnoreQueryFilters().Include(p=>p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId &&
                    p.WorkspaceId == _context.CurrentWorkspaceId);
            return product;
        }

        public async Task<PagedList<Product>> SearchProductsByName(ProductSortType sortType 
            ,string productName, int pageNumber, int pageSize)
        {
            var normalizedName = productName.Trim().ToLower();

            var query = _context.Products.Include(p => p.Category).Where
                (p=>p.ProductName.ToLower().Contains(normalizedName));

            var totalCount = await query.CountAsync();

            query = ApplySorting(query,sortType);

            var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<Product>(products, totalCount, pageNumber, pageSize);
        }
        public async Task<PagedList<Product>> SearchProductsByNameAsAdmin(ProductSortType sortType ,
            string productName, int pageNumber, int pageSize)
        {
            var normalizedName = productName.Trim().ToLower();

            var query = _context.Products.Include(p => p.Category).IgnoreQueryFilters().Where
                (p => p.WorkspaceId == _context.CurrentWorkspaceId &&
                    p.ProductName.ToLower().Contains(normalizedName));

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, sortType);

            var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<Product>(products, totalCount, pageNumber, pageSize);
        }
        public async Task AddProduct(Product newProduct)
        {
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product newProduct)
        {
            _context.Products.Update(newProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task AddStock(int id, int quantity)
        {
            var product = await GetProductById(id);
            if (product is null)
                throw new InvalidOperationException($"Product {id} was not found.");

            product.AddStock(quantity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsProductExist(int productId)
        {
            var isProductExist = await _context.Products.AnyAsync(p => p.Id == productId);
            return isProductExist;
        }

        private IQueryable<Product> ApplySorting(IQueryable<Product> query, ProductSortType sortType)
        {
            return sortType switch
            {
                ProductSortType.LowToHigh => query.OrderBy(p => p.Price),
                ProductSortType.HighToLow => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Id)
            };
        }
    }
}
