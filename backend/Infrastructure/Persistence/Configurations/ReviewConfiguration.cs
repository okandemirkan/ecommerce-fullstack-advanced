using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.User)
                .WithMany(r => r.Reviews).HasForeignKey(r=>r.UserId);
            builder.HasOne(r => r.Product)
                .WithMany(r => r.Reviews).HasForeignKey(r=>r.ProductId);

            builder.Property(r => r.Comment).HasMaxLength(2000);
            builder.Property(r=>r.Rating).IsRequired();

            builder.HasIndex(r => new { r.UserId, r.ProductId })
               .IsUnique();
        }
    }
}
