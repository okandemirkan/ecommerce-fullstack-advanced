using Application.Interfaces;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _context;
        public OrderRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<Order?> GetOrderById(int id)
        {
            var order = await _context.Orders.Include(o => o.User).Include(o => o.Items).
                ThenInclude(i=>i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public async Task<PagedList<Order>>GetLastOrders(int pageNumber,int pageSize)
        {
            var totalCount = await _context.Orders.CountAsync();

            var orders = await _context.Orders.Include(o=>o.User).
                Include(o=>o.Items).OrderByDescending(o => o.CreatedAt).Skip((pageNumber-1)* pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedList<Order>(orders,totalCount,pageNumber,pageSize);
        }
        public async Task AddOrder(Order Order)
        {
            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateOrder(Order Order)
        {
            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();
        }
        public async Task<PagedList<Order>> GetOrdersByUserId(int userId,int pageNumber, int pageSize)
        {
            var totalCount = await _context.Orders.CountAsync(o=>o.UserId == userId);

            var orders = await _context.Orders.Include(o => o.User).Include(o=>o.Items).
                Where(o => o.UserId == userId).OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            
            return new PagedList<Order>(orders,totalCount,pageNumber,pageSize);
        }

        public async Task<bool> IsProductUsedInAnyOrder(int productId)
        {
            var hasorder = await _context.OrderItems.AnyAsync(o=>o.ProductId == productId);
            return hasorder;
        }
    }
}
