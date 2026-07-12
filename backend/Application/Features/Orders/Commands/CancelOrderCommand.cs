using Application.DTOs.OrderDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record CancelOrderCommand(int orderId) : IRequest<Result<OrderDTO>>;
}
