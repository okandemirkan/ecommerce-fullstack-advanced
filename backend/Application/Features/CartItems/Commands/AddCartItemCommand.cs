using Application.DTOs.CartItemDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Commands
{
    public record AddCartItemCommand(int userId,AddCartItemDTO CartItem) : IRequest<Result<CartItemDTO>>;
}
