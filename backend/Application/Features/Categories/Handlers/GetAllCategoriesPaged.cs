using Application.DTOs.CategoryDTOs;
using Application.Features.Categories.Queries;
using Application.Interfaces;
using Application.Pagination;
using Application.Result;
using MediatR;

namespace Application.Features.Categories.Handlers
{
    public class GetAllCategoriesPagedHandler : IRequestHandler<GetAllCategoriesPagedQuery,
        Result<PagedList<CategoryDTO>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesPagedHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<PagedList<CategoryDTO>>> Handle(GetAllCategoriesPagedQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategoriesPaged(request.pageNumber, request.pageSize);

            var mappedItems = categories.Items.Select(c => new CategoryDTO(c.Id, c.CategoryName)).ToList();

            var result = new PagedList<CategoryDTO>(mappedItems, categories.TotalCount, request.pageNumber,
                request.pageSize);

            return Result<PagedList<CategoryDTO>>.Success("Categories retrived successfully",
                result);
        }
    }
}
