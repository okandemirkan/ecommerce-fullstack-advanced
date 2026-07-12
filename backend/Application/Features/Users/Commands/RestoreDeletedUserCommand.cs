using Application.Result;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record RestoreDeletedUserCommand(int userId) : IRequest<Result<object>>;
}
