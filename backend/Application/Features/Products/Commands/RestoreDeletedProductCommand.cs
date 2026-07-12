using Application.DTOs.ProductDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record RestoreDeletedProductCommand(int productId) : IRequest<Result<object>>;
}
