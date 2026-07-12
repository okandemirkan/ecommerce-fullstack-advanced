using Application.DTOs.OrderDTOs;
using Application.Features.Orders.Queries;
using Application.Interfaces;
using Application.Pagination;
using Application.Result;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Handlers.Get
{
    public class GetLastOrdersHandler : IRequestHandler<GetLastOrdersQuery, 
        Result<PagedList<OrderDTO>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetLastOrdersHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<OrderDTO>>> Handle(GetLastOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetLastOrders(request.pageNumber,request.pageSize);

            var mappedOrders = _mapper.Map<List<OrderDTO>>(orders.Items);

            var result = new PagedList<OrderDTO>(mappedOrders,orders.TotalCount,request.pageNumber,request.pageSize);

            return Result<PagedList<OrderDTO>>.Success($"Last {request.pageSize} order retrived successfully",result);
        }
    }
}
