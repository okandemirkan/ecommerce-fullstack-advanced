using Application.DTOs.ReviewDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Reviews.Commands
{
    public record AddReviewCommand(int userId,ReviewRequestDTO reviewDto) 
        : IRequest<Result<ReviewResponseDTO>>;
}
