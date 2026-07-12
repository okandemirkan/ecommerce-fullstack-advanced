using Application.Result;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record SoftDeleteUserCommand(int userId) : IRequest<Result<object>>;
}
