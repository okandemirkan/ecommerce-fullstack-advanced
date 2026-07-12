using Application.DTOs.OrderDTOs;
using Application.Pagination;
using Application.Result;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public record GetLastOrdersQuery(int pageNumber, int pageSize) :
        IRequest<Result<PagedList<OrderDTO>>>;
}
