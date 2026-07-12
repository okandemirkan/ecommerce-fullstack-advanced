using Application.Result;
using MediatR;

namespace Application.Features.Users.Queries
{
    public record IsUserActiveQuery(int userId) : IRequest<Result<bool>>;
}
