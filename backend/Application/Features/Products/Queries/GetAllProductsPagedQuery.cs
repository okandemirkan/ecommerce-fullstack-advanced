using Application.DTOs.ProductDTOs;
using Application.Enums;
using Application.Pagination;
using Application.Result;
using MediatR;
namespace Application.Features.Products.Queries
{
    public record GetAllProductsPagedQuery(ProductSortType sortType,int pageNumber,int pageSize) 
        : IRequest<Result<PagedList<ProductResponseDTO>>>;
    
}
