
using Application.Result;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record LoginCommand(string email, string password) : IRequest<Result<string>>;
}
