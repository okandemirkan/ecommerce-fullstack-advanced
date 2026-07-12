using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Quantity).IsRequired();

            builder.HasOne(c => c.User)
                .WithMany(c => c.CartItems).HasForeignKey(c=>c.UserId).IsRequired();

            builder.HasOne(c => c.Product)
                .WithMany(c => c.CartItems).HasForeignKey(c=>c.ProductId).IsRequired();

            builder.HasIndex(c=> new {c.UserId, c.ProductId})
                .IsUnique();
        }
    }
}
