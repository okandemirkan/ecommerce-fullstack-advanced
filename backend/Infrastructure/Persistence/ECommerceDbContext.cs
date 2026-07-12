using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Reflection;
namespace Infrastructure.Persistence
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem>OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review>Reviews { get; set; }
        public DbSet<Category> Categories { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
