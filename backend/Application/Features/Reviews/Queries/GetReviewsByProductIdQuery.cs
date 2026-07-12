using Application.DTOs.ReviewDTOs;
using Application.Result;
using MediatR;

namespace Application.Features.Reviews.Queries
{
    public record GetReviewsByProductIdQuery(int productId) : 
        IRequest<Result<IEnumerable<GetReviewsResponseDTO>>>;

}
