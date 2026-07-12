namespace Application.DTOs.OrderItemDTOs
{
    public record OrderItemDTO(int OrderItemId,int productId,string ProductName,string? ImageUrl ,decimal Price, 
        int Quantity);
}
