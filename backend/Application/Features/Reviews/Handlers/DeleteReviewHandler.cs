using Application.Exceptions;
using Application.Features.Reviews.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Reviews.Handlers
{
    public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand, Result<object>>
    {
        private readonly IReviewRepository _reviewRepository;

        public DeleteReviewHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Result<object>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetReviewById(request.reviewId);
            if (review is null)
                throw new NotFoundException("No review found with provided Id");
            if (review.UserId != request.userId)
                throw new ForbiddenException("You can only delete your own reviews.");

            await _reviewRepository.DeleteReview(review);
            return Result<object>.Success("Review deleted successfully");
        }
    }
}
