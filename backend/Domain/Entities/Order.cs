using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Order : WorkspaceEntity<int>
    {
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
        public string ShippingAddress { get; private set; } = string.Empty;
        public decimal TotalPrice { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        private Order() { }
        public static Order CreateOrder(User user, Address address, Product product, int quantity)
        {
            if (quantity < 0)
                throw new DomainException("Quantity must be greater than 0");

            var order = new Order()
            {
                UserId = user.Id,
                User = user,
                ShippingAddress = $"{address.City}, {address.District}, {address.FullAddress}",
                OrderStatus = OrderStatus.Pending,
            };

            order.AddItem(product, quantity);
            return order;
        }
        public static Order CreateWorkspaceCopy(int userId, string shippingAddress,
            decimal totalPrice, OrderStatus orderStatus, Guid workspaceId)
        {
            var order = new Order
            {
                UserId = userId,
                ShippingAddress = shippingAddress,
                TotalPrice = totalPrice,
                OrderStatus = orderStatus
            };
            order.AssignToWorkspace(workspaceId);
            return order;
        }

        public void AddWorkspaceItem(OrderItem item)
        {
            _items.Add(item);
        }
        public void AddItem(Product product, int quantity)
        {
            if (OrderStatus != OrderStatus.Pending)
                throw new DomainException("Items can only be added to pending orders.");
            if (quantity < 0)
                throw new DomainException("Quantity cannot be negative");
            if (product.Stock < quantity)
                throw new DomainException("Insufficient stock for this product.");
            product.DecreaseStock(quantity);

            var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }

            else
            {
                var orderItem = OrderItem.CreateOrderItem(
                    product.Id,
                    product.ProductName,
                    product.ImageUrl,
                    product.Price,
                    quantity
                );
                _items.Add(orderItem);
            }

            RecalculateTotalPrice();
        }
        public void RemoveItem(int orderItemId)
        {
            if (OrderStatus != OrderStatus.Pending)
                throw new DomainException("Items can only be removed from pending orders.");

            var item = _items.FirstOrDefault(i => i.Id == orderItemId);
            if (item is null)
                throw new DomainException("Item not found in order.");

            if (_items.Count == 1)
                throw new DomainException("Order must contain at least one item.");

            _items.Remove(item);
            RecalculateTotalPrice();
        }

        private void RecalculateTotalPrice()
        {
            TotalPrice = _items.Sum(i => i.TotalPrice);
        }
        public void UpdateShippingAddress(string city,string district, string fullAddress) 
            => ShippingAddress = $"{city}, {district}, {fullAddress}";
        public void ShipOrder()
        {
            if (OrderStatus == OrderStatus.Shipped)
                throw new DomainException("The order has already been shipped.");
            if (OrderStatus == OrderStatus.Canceled)
                throw new DomainException("Canceled orders cannot be shipped.");
            if (OrderStatus == OrderStatus.Delivered)
                throw new DomainException("Delivered orders cannot be shipped.");

            OrderStatus = OrderStatus.Shipped;
        }
        public void DeliverOrder()
        {
            if (OrderStatus == OrderStatus.Delivered)
                throw new DomainException("The order has already been delivered.");
            if (OrderStatus == OrderStatus.Canceled)
                throw new DomainException("Canceled orders cannot be delivered.");
            if (OrderStatus == OrderStatus.Pending)
                throw new DomainException("Pending orders cannot be delivered without being shipped.");

            OrderStatus = OrderStatus.Delivered;
        }
        public void CancelOrder()
        {
            if (OrderStatus == OrderStatus.Delivered)
                throw new DomainException("Delivered orders cannot be cancelled.");
            if (OrderStatus == OrderStatus.Canceled)
                throw new DomainException("The order has already been cancelled.");

            foreach(var item in _items)
            {
                item.Product.AddStock(item.Quantity);
            }

            OrderStatus = OrderStatus.Canceled;
        }
    }
}
