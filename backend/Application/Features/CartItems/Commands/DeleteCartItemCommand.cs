using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Commands
{
    public record DeleteCartItemCommand(int userId,int cartItemId) : IRequest<Result<object>>;
}
