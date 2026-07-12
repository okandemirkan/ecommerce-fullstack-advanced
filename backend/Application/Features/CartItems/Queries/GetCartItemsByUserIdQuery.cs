using Application.DTOs.CartItemDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Queries
{
    public record GetCartItemsByUserIdQuery(int userId) : IRequest<Result<CartDTO>>;
}
