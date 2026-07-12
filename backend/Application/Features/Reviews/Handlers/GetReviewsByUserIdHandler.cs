using Application.DTOs.ReviewDTOs;
using Application.Exceptions;
using Application.Features.Reviews.Queries;
using Application.Interfaces;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Reviews.Handlers
{
    public class GetReviewsByUserIdHandler : IRequestHandler<GetReviewsByUserIdQuery,
        Result<IEnumerable<GetReviewsResponseDTO>>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetReviewsByUserIdHandler(IReviewRepository reviewRepository, IUserRepository userRepository, 
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetReviewsResponseDTO>>> Handle(GetReviewsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var isUserExist = await _userRepository.IsUserActive(request.userId);
            if (!isUserExist)
                throw new NotFoundException("No user found with provided Id");

            var reviews = await _reviewRepository.GetReviewsByUserId(request.userId);

            var result = _mapper.Map<List<GetReviewsResponseDTO>>(reviews);

            return Result<IEnumerable<GetReviewsResponseDTO>>.Success("Reviews retrieved successfully.",
                result);
        }
    }
}
