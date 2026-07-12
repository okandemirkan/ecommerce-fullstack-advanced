using Application.DTOs.ProductDTOs;
using Application.Enums;
using Application.Pagination;
using Application.Result;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetProductsByCategoryIdQuery(ProductSortType sortType,int categoryId,int pageNumber,int pageSize) 
        : IRequest<Result<PagedList<ProductResponseDTO>>>;
}
