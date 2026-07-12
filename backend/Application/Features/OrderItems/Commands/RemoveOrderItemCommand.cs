using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.OrderItems.Commands
{
    public record RemoveOrderItemCommand(int userId, RemoveOrderItemDTO dto) 
        : IRequest<Result<OrderDTO>>;
}
