using Domain.Exceptions;
namespace Domain.Entities
{
    public class Product : WorkspaceEntity<int>
    {
        public string ProductName { get; private set; } = string.Empty;
        public int CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;
        public string? Description { get; private set; }
        public string? ImageUrl { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        private readonly List<CartItem> _cartItems = new();
        public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

        private readonly List<Review> _reviews = new();
        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
        private Product() { }
        public static Product AddProduct(string productName,int categoryId ,string? description, string? imageUrl
            , decimal price, int stock)
        {
            Validate(productName, price, stock);

            return new Product()
            {
                ProductName = productName,
                CategoryId = categoryId,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                Stock = stock,
            };
        }

        public void UpdateProduct(string productName,int categoryId ,string? description, string? imageUrl
            , decimal price, int stock)
        {
            Validate(productName, price, stock);

            ProductName = productName;
            CategoryId = categoryId;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            Stock = stock;
        }
        public void UpdateImage(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new DomainException("Image Url cannot be empty");
            ImageUrl = imageUrl;
        }

        public void RemoveProductImage() => ImageUrl = null;
        private static void Validate(string productName, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new DomainException("Product Name cannot be empty");
            if (price < 0)
                throw new DomainException("Price cannot be negative");
            if (stock < 0)
                throw new DomainException("Stock cannot be negative");
        }
        public void DecreaseStock(int quantity)
        {
            if (quantity < 0)
                throw new DomainException("Quantity cannot be negative.");
            if (Stock < quantity)
                throw new DomainException("Not enough products available.");
            Stock -= quantity;
        }
        public void AddStock(int quantity)
        {
            if (quantity < 0)
                throw new DomainException("Quantity cannot be negative.");
            Stock += quantity;
        }
    }
}
