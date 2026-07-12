using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.ImageUrl).HasMaxLength(3000);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(15,2)");
            builder.Property(p => p.Stock).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}
