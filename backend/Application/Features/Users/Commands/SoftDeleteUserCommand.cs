using Application.Result;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record SoftDeleteUserCommand(int UserId, int CurrentUserId) : IRequest<Result<object>>;
}
