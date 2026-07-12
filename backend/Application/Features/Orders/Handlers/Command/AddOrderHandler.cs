using Application.Features.Orders.Commands;
using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Domain.Entities;
using Application.Result;
using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;

namespace Application.Features.Orders.Handlers.Command
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand,Result<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public AddOrderHandler(IOrderRepository orderRepository,
            IUserRepository userRepository,IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }
        public async Task<Result<OrderDTO>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var dto = request.addOrderDto;
            var user = await _userRepository.GetUserById(request.userId);
            var product = await _productRepository.GetProductById(dto.productId);
            if (product is null)
                throw new NotFoundException("No product found with provided Id.");
            if (user is null)
                throw new NotFoundException("No user found with provided Id.");
            var address = user.Addresses.FirstOrDefault(a => a.Id == dto.addressId);
            if (address is null)
                throw new NotFoundException("No address found with provided Id.");
                                                                                                    
            var order = Order.CreateOrder(user,address,product, dto.quantity);
            await _orderRepository.AddOrder(order);
            await _productRepository.UpdateProduct(product);

            var result = new OrderDTO(order.Id,user.Id,user.Username,order.ShippingAddress,order.TotalPrice,order.CreatedAt,
                order.OrderStatus,order.Items.Select(i=>new OrderItemDTO(i.Id,i.ProductId,i.ProductName,i.ImageUrl,i.Price,i.Quantity)));

            return Result<OrderDTO>.Success("Order created successfully"
                , result);
        }
    }
}
