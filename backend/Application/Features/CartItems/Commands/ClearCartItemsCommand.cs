using Application.Result;
using MediatR;

namespace Application.Features.CartItems.Commands
{
    public record ClearCartItemsCommand(int userId) : IRequest<Result<object>>;
}
