namespace Application.DTOs.ReviewDTOs
{
    public record GetReviewsResponseDTO(int ReviewId,int ProductId,string ProductName,int UserId,string UserName,
        byte Rating,string? Comment, DateTime CreatedAt);
}
