namespace Application.DTOs.OrderItemDTOs
{
    public record AddOrderItemDTO(int OrderId, int ProductId, int Quantity);
}
