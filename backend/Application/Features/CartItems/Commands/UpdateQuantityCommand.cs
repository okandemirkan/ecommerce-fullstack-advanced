using Application.DTOs.CartItemDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Commands
{
    public record UpdateQuantityCommand(int cartItemId,int quantity) : IRequest<Result<CartItemDTO>>;
}
