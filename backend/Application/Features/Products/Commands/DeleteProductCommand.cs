using Application.Result;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record DeleteProductCommand(int productId) : IRequest<Result<object>>;
}
