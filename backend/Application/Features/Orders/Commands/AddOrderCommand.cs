using Application.DTOs.OrderDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record AddOrderCommand(int userId,AddOrderDTO addOrderDto) : IRequest<Result<OrderDTO>>;
}
