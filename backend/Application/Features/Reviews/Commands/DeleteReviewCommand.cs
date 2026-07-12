using Application.Result;
using MediatR;

namespace Application.Features.Reviews.Commands
{
    public record DeleteReviewCommand(int userId, int reviewId) : IRequest<Result<object>>;
}
