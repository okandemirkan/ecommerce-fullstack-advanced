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
    public class UpdateOrderAddressHandler : IRequestHandler<UpdateOrderAddressCommand, Result<OrderDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAddressRepository _addressRepository;
        public UpdateOrderAddressHandler(IUserRepository userRepository, IOrderRepository orderRepository,
            IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
        }

        public async Task<Result<OrderDTO>> Handle(UpdateOrderAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.userId);
            if (user is null)
                throw new NotFoundException("No user found with provided Id");

            var order = await _orderRepository.GetOrderById(request.dto.OrderId);
            if (order is null || order.UserId != request.userId)
                throw new NotFoundException("No order found with provided Id");
            if (order.OrderStatus != OrderStatus.Pending)
                throw new BadRequestException("Order address can only be updated while the order is in Pending status.");

            var address = await _addressRepository.GetAddressById(request.dto.AddressId);
            if (address is null || address.UserId != request.userId)
                throw new NotFoundException("No address found with provided Id");

            order.UpdateShippingAddress(address.City, address.District, address.FullAddress);
            await _orderRepository.UpdateOrder(order);

            var result = new OrderDTO(order.Id,user.Id,user.Username,order.ShippingAddress,order.TotalPrice,
                order.CreatedAt,order.OrderStatus,order.Items.Select(i=> new OrderItemDTO(
                    i.Id,i.ProductId,i.ProductName,i.ImageUrl,i.Price,i.Quantity)));

            return Result<OrderDTO>.Success("Shipping address updated successfully",result);

        }
    }
}
