using Application.DTOs.ProductDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record AddStockCommand(int Id, int Quantity) : IRequest<Result<ProductResponseDTO>>;
}
