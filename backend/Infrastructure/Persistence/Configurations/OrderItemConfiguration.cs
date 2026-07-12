using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(o => o.OrderId);

            builder.HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(o => o.ImageUrl).HasMaxLength(3000);
            builder.Property(o => o.Price).IsRequired().HasColumnType("decimal(15,2)");
            builder.Property(o => o.Quantity).IsRequired();
            builder.Ignore(o => o.TotalPrice);
        }
    }
}
