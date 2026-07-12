using Application.DTOs.AddressDTOs;
using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using Application.Features.OrderItems.Commands;
using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using Domain.Enums;
using EComerceWebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EComerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Order-By-OrderId/{id}")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            var response = await _mediator.Send(new GetOrderByIdQuerie(id));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Last-Orders")]
        public async Task<ActionResult> GetLastOrders(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new GetLastOrdersQuery(pageNumber,pageSize));
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("Get-Order-By-UserId/{id}")]
        public async Task<ActionResult> GetOrdersByUserId(int id ,
            [FromQuery]int pageNumber = 1 ,
            [FromQuery]int pageSize = 10)
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery(id,pageNumber,pageSize));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("Get-Current-User-Orders")]
        public async Task<ActionResult> GetCurrentUserOrders(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery(User.GetUserId(),pageNumber,pageSize));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Order")]
        public async Task<ActionResult> AddOrder(AddOrderCommand command)
        {
            var orderid = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderid }, orderid);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost("Add-Order-To-Current-User")]
        public async Task<ActionResult> AddOrderToCurrentUser(AddOrderDTO dto)
        {
            var orderid = await _mediator.Send(
                new AddOrderCommand(User.GetUserId(), dto));
            return CreatedAtAction(nameof(GetOrderById), new { id = orderid }, orderid);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Order-Item")]
        public async Task<ActionResult> AddOrderItem(AddOrderItemCommand command)
        {
            var orderid = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderid }, orderid);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost("Add-Order-Item-To-Current-User")]
        public async Task<ActionResult> AddOrderItemToCurrentUser(AddOrderItemDTO dto)
        {
            var orderid = await _mediator.Send(
                new AddOrderItemCommand(User.GetUserId(), dto));
            return CreatedAtAction(nameof(GetOrderById), new { id = orderid }, orderid);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPatch("Update-Shipping-Address")]
        public async Task<ActionResult> UpdateShippingAddress(UpdateOrderAddressDTO dto)
        {
            var response = await _mediator.Send(new UpdateOrderAddressCommand(User.GetUserId(), dto));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update-Shipping-Address-As-Admin")]
        public async Task<ActionResult> UpdateShippingAddressAsAdmin(int userId, UpdateOrderAddressDTO dto)
        {
            var response = await _mediator.Send(new UpdateOrderAddressCommand(userId, dto));
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Update-Order-Status/{orderId}")]
        public async Task<ActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var response = await _mediator.Send(new UpdateOrderStatusCommand(orderId, status));
            return Ok(response);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPatch("Cancel-Order")]
        public async Task<ActionResult> CancelOrder(int orderId)
        {
            var response = await _mediator.Send(new CancelOrderCommand(orderId));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete("Remove-Order-Item")]
        public async Task<ActionResult> RemoveOrderItem(RemoveOrderItemDTO dto)
        {
            var response = await _mediator.Send(new RemoveOrderItemCommand(User.GetUserId(), dto));
            return Ok(response);
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpDelete("Remove-Order-Item-As-Admin")]
        public async Task<ActionResult> RemoveOrderItem(int userId, RemoveOrderItemDTO dto)
        {
            var response = await _mediator.Send(new RemoveOrderItemCommand(userId, dto));
            return Ok(response);
        }
    }
}
