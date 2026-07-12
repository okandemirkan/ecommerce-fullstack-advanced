using Application.DTOs.CategoryDTOs;
using Application.Exceptions;
using Application.Features.Categories.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Categories.Handlers
{
    public class UpdateCategoryNameHandler : IRequestHandler<UpdateCategoryNameCommand, Result<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryNameHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryDTO>> Handle(UpdateCategoryNameCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryById(request.dto.CategoryId);
            if (category is null)
                throw new NotFoundException("No category found with provided Id");

            category.UpdateCategoryName(request.dto.CategoryName);
            await _categoryRepository.UpdateCategory(category);

            var result = new CategoryDTO(category.Id,request.dto.CategoryName);

            return Result<CategoryDTO>.Success("Category updated succesffully. New category name :",
                result);
        }
    }
}
