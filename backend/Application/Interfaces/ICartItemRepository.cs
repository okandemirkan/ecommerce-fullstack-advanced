using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetCartItemById(int id);
        Task<CartItem?> GetCartItemByProductId(int userId,int productId);
        Task<IEnumerable<CartItem>> GetCartItemsByUserId(int userId);
        Task<IEnumerable<CartItem>> GetCartItemsWithProductsByUserId(int userId);
        Task AddCartItem(CartItem cartItem);
        Task UpdateCartItem(CartItem cartItem);
        Task DeleteCartItem(CartItem cartItem);
        Task ClearCart(IEnumerable<CartItem> cartItem);
    }
}
