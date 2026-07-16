using Application.Exceptions;
using Application.Features.Categories.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Categories.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Result<object>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<object>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryById(request.categoryId);
            if (category is null)
                throw new NotFoundException("No category found with provided Id");
            if (await _categoryRepository.HasProducts(request.categoryId))
                throw new BadRequestException("Category cannot be deleted because it contains products.");

            await _categoryRepository.DeleteCategory(category);

            return Result<object>.Success("Category deleted successfully");
        }
    }
}
