using Application.DTOs.ReviewDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Reviews.Commands
{
    public record UpdateReviewCommand(int userId,int reviewId, UpdateReviewDTO review) 
        : IRequest<Result<ReviewResponseDTO>>;
}
