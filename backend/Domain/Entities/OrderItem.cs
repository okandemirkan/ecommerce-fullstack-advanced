using Domain.Exceptions;

namespace Domain.Entities
{
    public class OrderItem : BaseEntity<int> // Bir siparişteki her bir ürünü temsil ediyor
    {
        public int OrderId { get; private set;}
        public Order Order { get; private set;}
        public int ProductId { get; private set;}
        public Product Product { get; private set;}
        public string ProductName { get; private set;} //Ürün bilgileri değiştiğinde sipariş bilgileri değişmemeli.
        public string? ImageUrl { get; private set;}
        public decimal Price { get; private set;}
        public int Quantity { get; private set;}
        public decimal TotalPrice => Price * Quantity;
        private OrderItem() { }

        public static OrderItem CreateOrderItem(int productId, string productName,string? imageurl,decimal price,int quantity)
        {
            if(quantity < 0)
                throw new DomainException("Quantity cannot be negative");
            if (price < 0)
                throw new DomainException("Price cannot be negative");
            if (string.IsNullOrWhiteSpace(productName))
                throw new DomainException("Product name cannot be empty");

            return new OrderItem
            {
                ProductId = productId,
                ProductName = productName,
                ImageUrl = imageurl,
                Price = price,
                Quantity = quantity
            };
        }
        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new DomainException("Amount must be greater than zero.");
            Quantity += amount;
        }
    }
}
