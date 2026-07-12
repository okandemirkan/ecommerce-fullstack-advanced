using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using Application.Exceptions;
using Application.Features.Orders.Commands;
using Application.Interfaces;
using Application.Result;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Handlers.Command
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand,
        Result<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderStatusHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<OrderDTO>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.orderId);
            if (order is null)
                throw new NotFoundException("No order found with provided Id");

            var status = request.status;

            if (status == OrderStatus.Canceled) order.CancelOrder();
            else if (status == OrderStatus.Delivered) order.DeliverOrder();
            else if (status == OrderStatus.Shipped) order.ShipOrder();
            else if (status == OrderStatus.Pending) throw new BadRequestException("Order status cannot be changed to Pending.");
            else throw new BadRequestException("Invalid order status.");

            var result = new OrderDTO(order.Id,order.UserId ,order.User.Username, order.ShippingAddress, order.TotalPrice,
                order.CreatedAt,order.OrderStatus, order.Items.Select(i => new OrderItemDTO
                (i.Id,i.ProductId,i.ProductName, i.ImageUrl, i.Price, i.Quantity)));

            await _orderRepository.UpdateOrder(order);

            return Result<OrderDTO>.Success("Order Status updated successfully",result);

        }
    }
}