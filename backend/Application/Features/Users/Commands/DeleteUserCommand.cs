using MediatR;
using Application.Result;
namespace Application.Features.Users.Commands
{
    public record DeleteUserCommand(int userId) : IRequest<Result<object>>;
}
