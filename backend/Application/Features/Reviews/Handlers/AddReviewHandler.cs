using Application.DTOs.ReviewDTOs;
using Application.Exceptions;
using Application.Features.Reviews.Commands;
using Application.Interfaces;
using Application.Result;
using Domain.Entities;
using MediatR;

namespace Application.Features.Reviews.Handlers
{
    public class AddReviewHandler : IRequestHandler<AddReviewCommand, Result<ReviewResponseDTO>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public AddReviewHandler(IReviewRepository reviewRepository, IProductRepository productRepository, 
            IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task<Result<ReviewResponseDTO>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var dto = request.reviewDto;

            var isUserExist = await _userRepository.IsUserActive(request.userId);
            if (!isUserExist)
                throw new NotFoundException("No user found with provided Id");
            var isProductExist = await _productRepository.IsProductExist(dto.ProductId);
            if (!isProductExist)
                throw new NotFoundException("No product found with provided Id");
            var isReviewExist = await _reviewRepository.IsReviewExist(request.userId,dto.ProductId);
            if (isReviewExist)
                throw new AlreadyExistException("You have already reviewed this product.");

            var review = Review.CreateReview(request.userId, dto.ProductId,dto.Comment, dto.Rating);
            await _reviewRepository.AddReview(review);

            var result = new ReviewResponseDTO(request.userId,review.ProductId,review.Comment,
                review.Rating);

            return Result<ReviewResponseDTO>.Success("Review Added Successfully", result);
        }
    }
}
