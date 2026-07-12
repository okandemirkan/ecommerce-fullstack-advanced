namespace Application.DTOs.CartItemDTOs
{
    public record CartDTO(IEnumerable<CartItemDTO> carts,decimal grandTotal);
}
