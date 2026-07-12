using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasIndex(c => c.CategoryName).IsUnique();

            builder.HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            builder.HasData(
                new
                {
                    Id = 1,
                    CategoryName = "Telefon",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = 2,
                    CategoryName = "Bilgisayar ve Tablet",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = 3,
                    CategoryName = "Bilgisayar Bileşenleri ve Aksesuarları",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = 4,
                    CategoryName = "Ses Sistemleri",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = 5,
                    CategoryName = "TV ve Görüntü Sistemleri",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = 6,
                    CategoryName = "Kamera ve Fotoğraf",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = 7,
                    CategoryName = "Ev Aletleri",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = 8,
                    CategoryName = "Oyun ve Konsol",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                },
                new
                {
                    Id = 9,
                    CategoryName = "Giyilebilir Teknoloji",
                    IsDeleted = false,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc)
                }
);
        }
    }
}
