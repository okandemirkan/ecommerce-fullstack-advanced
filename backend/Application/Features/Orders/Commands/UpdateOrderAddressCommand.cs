using Application.DTOs.OrderDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record UpdateOrderAddressCommand(int userId, UpdateOrderAddressDTO dto)
        : IRequest<Result<OrderDTO>>;
}
