using Application.DTOs.CategoryDTOs;
using Application.Features.Categories.Commands;
using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Handlers
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Result<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryDTO>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = Category.AddCategory(request.categoryName);
            await _categoryRepository.AddCategory(category);
            var result = new CategoryDTO(category.Id,category.CategoryName);

            return Result<CategoryDTO>.Success($"New category added successfully",result);
        }
    }
}
