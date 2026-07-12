using Application.DTOs.ProductDTOs;
using Application.Result;
using MediatR;
namespace Application.Features.Products.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Result<ProductResponseDTO>>;
}
