using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.TotalPrice).IsRequired().HasColumnType("decimal(15,2)");
            builder.Property(o => o.OrderStatus).IsRequired().HasConversion<string>()
                .HasMaxLength(20);
            builder.Property(o => o.ShippingAddress).IsRequired().HasMaxLength(200);

            builder.HasOne(o => o.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.UserId);

            builder.HasMany(o => o.Items)
                .WithOne(o => o.Order)
                .HasForeignKey(o=>o.OrderId);
            
        }
    }
}
