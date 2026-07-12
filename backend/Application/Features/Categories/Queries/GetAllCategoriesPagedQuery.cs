using Application.DTOs.CategoryDTOs;
using Application.Pagination;
using Application.Result;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public record GetAllCategoriesPagedQuery(int pageNumber, int pageSize) :
        IRequest<Result<PagedList<CategoryDTO>>>;
}
