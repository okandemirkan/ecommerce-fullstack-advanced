using Domain.Entities;
namespace Application.Interfaces
{
    public interface IReviewRepository
    {
        Task AddReview(Review review);
        Task UpdateReview(Review review);
        Task DeleteReview(Review review);
        Task<bool> IsReviewExist(int userId,int productId);
        Task<IEnumerable<Review>?> GetReviewsByUserId(int userId);
        Task<IEnumerable<Review>?> GetReviewsByProductId(int productId);
        Task<Review?> GetReviewById(int reviewId);
    }
}
