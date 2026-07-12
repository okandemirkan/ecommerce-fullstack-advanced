using Application.DTOs.ReviewDTOs;
using Application.Exceptions;
using Application.Features.Reviews.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Reviews.Handlers
{
    public class UpdateReviewHandler : IRequestHandler<UpdateReviewCommand, Result<ReviewResponseDTO>>
    {
        private readonly IReviewRepository _reviewRepository;

        public UpdateReviewHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<Result<ReviewResponseDTO>> Handle(UpdateReviewCommand request, 
            CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetReviewById(request.reviewId);
            if (review is null)
                throw new NotFoundException("No review found with provided Id");
            if (review.UserId != request.userId)
                throw new ForbiddenException("You can only update your own reviews.");

            review.UpdateReview(request.review.Rating,request.review.Comment);
            await _reviewRepository.UpdateReview(review);

            var result = new ReviewResponseDTO(review.UserId, review.ProductId, request.review.Comment
                , request.review.Rating);
            return Result<ReviewResponseDTO>.Success("Review updated successfully.",result);
        }
    }
}
