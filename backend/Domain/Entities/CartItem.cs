using Domain.Exceptions;

namespace Domain.Entities
{
    public class CartItem : WorkspaceEntity<int>
    {
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public int ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
        public int Quantity {  get; private set; }
        public decimal TotalPrice => Quantity * Product.Price;
        private CartItem() { }
        public static CartItem CreateCart(int userId,Product product,int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than 0");

            return new CartItem()
            {
                UserId = userId,
                Product = product,
                ProductId = product.Id,
                Quantity = quantity
            };
        }

        public void AddQuantity(int quantity)
        {
            if (Product.Stock < quantity + Quantity)
                throw new DomainException("Insufficient stock");
            Quantity += quantity;
        }
        
        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than 0");
            if (Product.Stock < quantity)
                throw new DomainException("Insufficient stock.");

            Quantity = quantity;
        }
    }
}
