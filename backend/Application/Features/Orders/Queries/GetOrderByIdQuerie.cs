using Application.DTOs.OrderDTOs;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public record GetOrderByIdQuerie(int id) : IRequest<OrderDTO>;
}
