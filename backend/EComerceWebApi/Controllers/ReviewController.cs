using Application.DTOs.ReviewDTOs;
using Application.Features.Reviews.Commands;
using Application.Features.Reviews.Queries;
using EComerceWebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EComerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles ="Customer,Admin")]
        [HttpGet("Get-Reviews")]
        public async Task<ActionResult>GetReviews()
        {
            var response = await _mediator.Send(new GetReviewsByUserIdQuery(User.GetUserId()));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Reviews-By-UserId/{userId}")]
        public async Task<ActionResult> GetReviewsByUserId(int userId)
        {
            var response = await _mediator.Send(new GetReviewsByUserIdQuery(userId));
            return Ok(response);
        }

        [HttpGet("Get-Reviews-By-ProductId/{productId}")]
        public async Task<ActionResult>GetReviewsByProductId(int productId)
        {
            var response = await _mediator.Send(new GetReviewsByProductIdQuery(productId));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost("Add-Review")]
        public async Task<ActionResult> AddReview(ReviewRequestDTO review)
        {
            var response = await _mediator.Send(new AddReviewCommand(User.GetUserId(),review));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPut("Update-Review/{reviewId}")]
        public async Task<ActionResult> UpdateReview(int reviewId, UpdateReviewDTO review)
        {
            var response = await _mediator.Send(new UpdateReviewCommand(User.GetUserId(),reviewId, review));
            return Ok(response);
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete("Delete-Review")]
        public async Task<ActionResult>DeleteReview(int reviewId)
        {
            var response = await _mediator.Send(new DeleteReviewCommand(User.GetUserId(), reviewId));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Review-As-Admin")]
        public async Task<ActionResult> DeleteReviewAsAdmin(int userId,int reviewId)
        {
            var response = await _mediator.Send(new DeleteReviewCommand(userId, reviewId));
            return Ok(response);
        }

    }
}
