using Application.DTOs.OrderItemDTOs;
using Domain.Enums;
namespace Application.DTOs.OrderDTOs
{
    public record OrderDTO(int OrderId,int UserId,string UserName, string ShippingAddress, decimal TotalPrice,
        DateTime CreatedAt, OrderStatus OrderStatus, IEnumerable<OrderItemDTO> Items);
}