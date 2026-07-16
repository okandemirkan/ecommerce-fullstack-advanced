using Application.Interfaces;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceDbContext _context;
        public CategoryRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Category>> GetAllCategoriesPaged(int pageNumber,int pageSize)
        {
            var totalCount = await _context.Categories.CountAsync();

            var categories = await _context.Categories.OrderBy(category => category.Id)
                .Skip((pageNumber-1)*pageSize).
                Take(pageSize).ToListAsync();

            return new PagedList<Category>(categories, totalCount, pageNumber, pageSize);
        }
        public async Task<Category?> GetCategoryById(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            return category;
        }
        public async Task<bool> HasProducts(int categoryId)
        {
            return await _context.Products
                .IgnoreQueryFilters()
                .AnyAsync(product => product.CategoryId == categoryId &&
                    product.WorkspaceId == _context.CurrentWorkspaceId);
        }
        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCategory(Category category) 
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

    }
}
