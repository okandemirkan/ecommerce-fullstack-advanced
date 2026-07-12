using Application.DTOs.ProductDTOs;
using Application.Pagination;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetSoftDeletedProductsPagedQuery(int pageNumber, int pageSize) :
        IRequest<Result<PagedList<ProductResponseDTO>>>;
}
