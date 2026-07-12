using Application.DTOs.ProductDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record UpdateProductImageCommand(int productId,string imageUrl) : IRequest<Result<ProductResponseDTO>>;
}
