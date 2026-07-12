using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using Application.Exceptions;
using Application.Features.Orders.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.Orders.Handlers.Command
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand,Result<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        public CancelOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<OrderDTO>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.orderId);
            if (order is null)
                throw new NotFoundException("No order found with provided Id");

            order.CancelOrder();

            await _orderRepository.UpdateOrder(order);

            var result = new OrderDTO(order.Id, order.UserId, order.User.Username, order.ShippingAddress, order.TotalPrice,
                order.CreatedAt, order.OrderStatus, order.Items.Select(i => new OrderItemDTO(i.Id,i.ProductId,i.ProductName,
                i.ImageUrl,i.Price,i.Quantity)));

            return Result<OrderDTO>.Success("Order canceled successfully",result);
        }
    }
}
