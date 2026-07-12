using Application.Result;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record SoftDeleteProductCommand(int productId) : IRequest<Result<object>>;
}
