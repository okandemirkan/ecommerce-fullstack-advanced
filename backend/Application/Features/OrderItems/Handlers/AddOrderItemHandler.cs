using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using Application.Exceptions;
using Application.Features.OrderItems.Commands;
using Application.Interfaces;
using Application.Result;
using MediatR;

namespace Application.Features.OrderItems.Handlers
{
    public class AddOrderItemHandler : IRequestHandler<AddOrderItemCommand, Result<OrderDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        public AddOrderItemHandler(IUserRepository userRepository, IProductRepository productRepository
            , IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Result<OrderDTO>> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.userId);
            if (user is null)
                throw new NotFoundException("User not found with provided Id");

            var dto = request.orderItem;

            var product = await _productRepository.GetProductById(dto.ProductId);
            if (product is null)
                throw new NotFoundException("Product not found with provided Id");

            var order = await _orderRepository.GetOrderById(dto.OrderId);
            if (order is null || order.UserId != user.Id)
                throw new NotFoundException("Order not found with provided Id");

            order.AddItem(product, dto.Quantity);
            await _orderRepository.UpdateOrder(order);
            await _productRepository.UpdateProduct(product);

            var result = new OrderDTO(order.Id,user.Id,user.Username, order.ShippingAddress, order.TotalPrice, order.CreatedAt,
                order.OrderStatus, order.Items.Select(i=>new OrderItemDTO(i.Id,i.ProductId,i.ProductName
                ,i.ImageUrl,i.Price,i.Quantity)));

            return Result<OrderDTO>.Success("Order item added successfully",result);
        }
    }
}
