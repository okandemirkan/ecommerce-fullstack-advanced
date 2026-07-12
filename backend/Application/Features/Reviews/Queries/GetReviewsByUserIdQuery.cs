using Application.DTOs.ReviewDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Reviews.Queries
{
    public record GetReviewsByUserIdQuery(int userId) : IRequest<Result<IEnumerable<GetReviewsResponseDTO>>>;
}
