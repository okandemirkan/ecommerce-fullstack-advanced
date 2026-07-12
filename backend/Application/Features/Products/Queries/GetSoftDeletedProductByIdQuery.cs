using Application.DTOs.ProductDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetSoftDeletedProductByIdQuery(int productId) : IRequest<Result<ProductResponseDTO>>;
}
