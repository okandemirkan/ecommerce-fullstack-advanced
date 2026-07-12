using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using Application.Exceptions;
using Application.Features.OrderItems.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.OrderItems.Handlers
{
    public class RemoveOrderItemHandler : IRequestHandler<RemoveOrderItemCommand, Result<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public RemoveOrderItemHandler(IOrderRepository orderRepository, IUserRepository userRepository, 
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<OrderDTO>> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.userId);
            if (user is null)
                throw new NotFoundException("No user found with provided Id");

            var order = await _orderRepository.GetOrderById(request.dto.OrderId);
            if (order is null || order.UserId != request.userId)
                throw new NotFoundException("No order found with provided Id");

            var orderItem = order.Items.FirstOrDefault(i=>i.Id == request.dto.OrderItemId);
            if (orderItem is null)
                throw new NotFoundException("No order item found with provided Id");

            var product = await _productRepository.GetProductById(orderItem.ProductId);
            if (product is null)
                throw new NotFoundException("No product item found with provided Id");

            order.RemoveItem(orderItem.Id);
            product.AddStock(orderItem.Quantity);

            await _orderRepository.UpdateOrder(order);
            await _productRepository.UpdateProduct(orderItem.Product);

            var result = new OrderDTO(order.Id,user.Id ,user.Username, order.ShippingAddress, order.TotalPrice,
                order.CreatedAt, order.OrderStatus, order.Items.Select(i=> 
                new OrderItemDTO(i.Id,i.ProductId,i.ProductName,i.ImageUrl,i.Price,i.Quantity)));

            return Result<OrderDTO>.Success("Order item removed successfully",result);

        }
    }
}
