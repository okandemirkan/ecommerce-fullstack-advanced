using Application.DTOs.CartItemDTOs;
using Application.Features.CartItems.Commands;
using Application.Features.CartItems.Queries;
using EComerceWebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EComerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("Get-Current-User-Items")]
        public async Task<ActionResult> GetCurrentUserItems()
        {
            var response = await _mediator.Send(new GetCartItemsByUserIdQuery(User.GetUserId()));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-User-Items/{id}")]
        public async Task<ActionResult> GetCartItemsByUserId(int id)
        {
            var response = await _mediator.Send(new GetCartItemsByUserIdQuery(id));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost("Add-Cart-Item")]
        public async Task<ActionResult> AddCartItem(AddCartItemDTO cartItem)
        {
            var response = await _mediator.Send(new AddCartItemCommand(User.GetUserId(),
                cartItem));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete("Delete-Current-User-Item/{cartItemId}")]
        public async Task<ActionResult> DeleteCartItem(int cartItemId)
        {
            var response = await _mediator.Send(new DeleteCartItemCommand(User.GetUserId(), cartItemId));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPatch("Update-Item-Quantity")]
        public async Task<ActionResult> UpdateQuantity(int cartItemId,int quantity)
        {
            var response = await _mediator.Send(new UpdateQuantityCommand(cartItemId, quantity));
            return Ok(response);
        }

        [Authorize(Roles ="Customer,Admin")]
        [HttpDelete("Clear-Current-User-CartItems")]
        public async Task<ActionResult> ClearCartItems()
        {
            var response = await _mediator.Send(new ClearCartItemsCommand(User.GetUserId()));
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Clear-User-CartItems/{id}")]
        public async Task<ActionResult> ClearCartItemsAsAdmin(int id)
        {
            var response = await _mediator.Send(new ClearCartItemsCommand(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Item/{cartItemId}")]
        public async Task<ActionResult> DeleteCartItemAsAdmin(int userId,int cartItemId)
        {
            var response = await _mediator.Send(new DeleteCartItemCommand(userId, cartItemId));
            return Ok(response);
        }
    }
}
