using Application.Features.Orders.Queries;
using Application.Interfaces;
using MediatR;
using Application.Exceptions;
using Application.Result;
using Application.DTOs.OrderDTOs;
using AutoMapper;
using Application.Pagination;
namespace Application.Features.Orders.Handlers.Get
{
    public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQuery,
        Result<PagedList<OrderDTO>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrdersByUserIdHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<Result<PagedList<OrderDTO>>> Handle(GetOrdersByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserId(request.userId,request.pageNumber,
                request.pageSize);
            if (orders is null)
                throw new NotFoundException("No order found with provided Id.");

            var mappedItems = _mapper.Map<List<OrderDTO>>(orders.Items);

            var result = new PagedList<OrderDTO>(mappedItems,orders.TotalCount,request.pageNumber,request.pageSize);

            return Result<PagedList<OrderDTO>>.Success("Orders retrieved successfully."
                , result);
        }
    }
}
