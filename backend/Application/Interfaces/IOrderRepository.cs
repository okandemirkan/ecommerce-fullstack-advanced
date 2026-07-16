using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderById(int id);
        Task<PagedList<Order>> GetLastOrders(int pageNumber, int pageSize);
        Task AddOrder(Order Order);
        Task UpdateOrder(Order Order);
        Task<PagedList<Order>> GetOrdersByUserId(int userId, int pageNumber, int pageSize);
        Task<bool> IsProductUsedInAnyOrder(int productId);
    }
}
