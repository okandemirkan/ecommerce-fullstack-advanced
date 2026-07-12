using Application.DTOs.OrderDTOs;
using Application.Result;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record UpdateOrderStatusCommand(int orderId, OrderStatus status) :
        IRequest<Result<OrderDTO>>;
}
