using Domain.Exceptions;

namespace Domain.Entities
{
    public class Review : BaseEntity<int>
    {
        public User User { get; private set;}
        public int UserId { get; private set;}
        public Product Product { get; private set;}
        public int ProductId { get; private set;}
        public string? Comment { get; private set;}
        public byte Rating { get; private set;}
        private Review(){ }

        public static Review CreateReview(int userId,int productId,string? comment,byte rating)
        {
            if (rating < 1 || rating > 5)
                throw new DomainException("Invalid Rating");
            return new Review() { 
               UserId = userId, ProductId = productId, Comment = comment,Rating = rating };
        }
        public void UpdateReview(byte rating, string? comment)
        {
            if (rating < 1 || rating > 5)
                throw new DomainException("Invalid Rating");

            Rating = rating;
            Comment = comment;
        }

    }
}
