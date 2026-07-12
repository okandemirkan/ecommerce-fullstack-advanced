namespace Application.DTOs.ReviewDTOs
{
    public record ReviewRequestDTO(int ProductId,string? Comment,byte Rating);
}
