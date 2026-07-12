namespace Application.DTOs.ProductDTOs
{
    public record ProductResponseDTO(int productId,string productName,int categoryId,string categoryName,string? description,string? imageUrl 
        ,decimal price,int stock);
}
