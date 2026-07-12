using Application.DTOs.ProductDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record UpdateProductCommand(int id, ProductRequestDTO productDTO)
        : IRequest<Result<ProductResponseDTO>>;
}