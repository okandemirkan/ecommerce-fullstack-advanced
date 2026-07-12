using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoleName).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(120);

            builder.HasIndex(r => r.RoleName).IsUnique();

            builder.HasData(
                new
                {
                    Id = 1,
                    RoleName = "Admin",
                    Description = "Full access.",
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 2,
                    RoleName = "Customer",
                    Description = "Standard customer access.",
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
                );
        }
    }
}
