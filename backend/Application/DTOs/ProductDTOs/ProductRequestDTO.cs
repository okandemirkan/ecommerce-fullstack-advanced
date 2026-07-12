namespace Application.DTOs.ProductDTOs
{
    public record ProductRequestDTO(string ProductName,int categoryId,string? Description, string? ImageUrl,
        decimal Price, int Stock);
}
