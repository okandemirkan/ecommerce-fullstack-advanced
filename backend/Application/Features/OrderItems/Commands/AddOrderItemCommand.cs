using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.OrderItems.Commands
{
    public record AddOrderItemCommand(int userId, AddOrderItemDTO orderItem) 
        : IRequest<Result<OrderDTO>>; 
}
