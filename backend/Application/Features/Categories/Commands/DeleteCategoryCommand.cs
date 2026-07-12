using Application.Result;
using MediatR;

namespace Application.Features.Categories.Commands
{
    public record DeleteCategoryCommand(int categoryId) : IRequest<Result<object>>;
}
