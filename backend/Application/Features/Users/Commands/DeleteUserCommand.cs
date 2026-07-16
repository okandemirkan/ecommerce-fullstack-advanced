using MediatR;
using Application.Result;
namespace Application.Features.Users.Commands
{
    public record DeleteUserCommand(int UserId, int CurrentUserId) : IRequest<Result<object>>;
}
