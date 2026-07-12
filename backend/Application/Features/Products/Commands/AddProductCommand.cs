using Application.DTOs.ProductDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record AddProductCommand(ProductRequestDTO Product) :
        IRequest<Result<ProductResponseDTO>>;
}
