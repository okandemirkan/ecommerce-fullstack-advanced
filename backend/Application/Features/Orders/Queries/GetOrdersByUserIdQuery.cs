using Application.DTOs.OrderDTOs;
using Application.Pagination;
using Application.Result;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public record GetOrdersByUserIdQuery(int userId, int pageNumber, int pageSize) 
        : IRequest<Result<PagedList<OrderDTO>>>;
}
