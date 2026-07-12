using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ECommerceDbContext _context;
        public ReviewRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<Review?> GetReviewById(int reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r=>r.Id == reviewId);
            return review;
        }
        public async Task<IEnumerable<Review>?> GetReviewsByUserId(int userId)
        {
            var reviews = await _context.Reviews.Where(r=> r.UserId == userId)
                .Include(r=>r.User).Include(r=>r.Product).ToListAsync();
            return reviews;
        }
        public async Task<IEnumerable<Review>?> GetReviewsByProductId(int productId)
        {
            var reviews = await _context.Reviews.Where(r => r.ProductId == productId)
                .Include(r => r.User).Include(r=>r.Product).ToListAsync();
            return reviews;
        }
        public async Task AddReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsReviewExist(int userId, int productId)
        {
            return await _context.Reviews.AnyAsync(r=> r.UserId == userId && 
            r.ProductId == productId);
        }
    }
}
