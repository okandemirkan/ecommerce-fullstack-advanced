using Application.Features.Orders.Queries;
using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Application.DTOs.OrderDTOs;
using Application.DTOs.OrderItemDTOs;
using AutoMapper;
namespace Application.Features.Orders.Handlers.Get
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuerie, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrderByIdHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<OrderDTO> Handle(GetOrderByIdQuerie request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.id);
            if (order is null)
                throw new NotFoundException("No order found with provided Id.");

            var orderDto = _mapper.Map<OrderDTO>(order);
            return orderDto;
        }
    }
}
