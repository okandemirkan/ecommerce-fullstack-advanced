using Application.DTOs.CategoryDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Categories.Commands
{
    public record AddCategoryCommand(string categoryName) : IRequest<Result<CategoryDTO>>;
}
