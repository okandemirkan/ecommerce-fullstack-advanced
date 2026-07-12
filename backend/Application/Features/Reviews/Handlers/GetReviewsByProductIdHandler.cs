using Application.DTOs.ReviewDTOs;
using Application.Exceptions;
using Application.Features.Reviews.Queries;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Reviews.Handlers
{
    public class GetReviewsByProductIdHandler : IRequestHandler<GetReviewsByProductIdQuery,
        Result<IEnumerable<GetReviewsResponseDTO>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public GetReviewsByProductIdHandler(IReviewRepository reviewRepository, IProductRepository productRepository, 
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetReviewsResponseDTO>>> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var isProductExist = await _productRepository.IsProductExist(request.productId);
            if (!isProductExist)
                throw new NotFoundException("No product found with provided Id.");
            
            var reviews = await _reviewRepository.GetReviewsByProductId(request.productId);

            var result = _mapper.Map<List<GetReviewsResponseDTO>>(reviews);

            return Result<IEnumerable<GetReviewsResponseDTO>>.
                Success("Reviews retrieved successfully.", result);
        }
    }
}
