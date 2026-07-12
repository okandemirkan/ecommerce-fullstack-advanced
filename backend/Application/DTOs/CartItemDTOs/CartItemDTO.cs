namespace Application.DTOs.CartItemDTOs
{
    public record CartItemDTO(int cartItemId,int productId,string productName,string? imageUrl,decimal price ,
        int quantity, decimal totalPrice);
}
