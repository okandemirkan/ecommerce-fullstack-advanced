using Application.DTOs.CategoryDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Categories.Commands
{
    public record UpdateCategoryNameCommand(CategoryDTO dto) : IRequest<Result<CategoryDTO>>;
}
