using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ECommerceDbContext _context;

        public CartItemRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> GetCartItemById(int id)
        {
            return await _context.CartItems.Include(c=>c.Product).FirstOrDefaultAsync(c=>c.Id == id);
        }
        public async Task<CartItem?> GetCartItemByProductId(int userId, int productId)
        {
            return await _context.CartItems.Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        }
        public async Task<IEnumerable<CartItem>> GetCartItemsWithProductsByUserId(int userId)
        {
            var cartItems = await _context.CartItems.Include(c => c.Product)
                .Where(c => c.UserId == userId).ToListAsync();
            return cartItems;
        }
        public async Task<IEnumerable<CartItem>> GetCartItemsByUserId(int userId)
        {
            var cartItems = await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();
            return cartItems;
        }
        public async Task AddCartItem(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCart(IEnumerable<CartItem> cartItem)
        {
            _context.CartItems.RemoveRange(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCartItem(CartItem cartItem)
        {
           _context.CartItems.Remove(cartItem);
           await _context.SaveChangesAsync();
        }
    }
}
