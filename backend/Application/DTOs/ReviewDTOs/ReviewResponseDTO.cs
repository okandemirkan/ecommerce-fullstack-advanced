namespace Application.DTOs.ReviewDTOs
{
    public record ReviewResponseDTO(int UserId, int ProductId, string? Comment, byte Rating);
}
