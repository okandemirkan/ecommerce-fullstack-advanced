using Application.Result;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record RemoveProductImageCommand(int productId) : IRequest<Result<object>>;
}
