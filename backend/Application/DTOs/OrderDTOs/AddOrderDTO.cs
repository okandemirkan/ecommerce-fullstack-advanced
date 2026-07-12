namespace Application.DTOs.OrderDTOs
{
    public record AddOrderDTO(int addressId, int productId,
        int quantity);
}
