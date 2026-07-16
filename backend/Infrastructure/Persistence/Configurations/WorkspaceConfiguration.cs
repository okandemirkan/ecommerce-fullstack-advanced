using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
    {
        public void Configure(EntityTypeBuilder<Workspace> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.IsDemo).IsRequired();
            builder.Property(w => w.IsStorefront).IsRequired().HasDefaultValue(false);
            builder.HasIndex(w => w.ExpiresAt);
            builder.HasIndex(w => w.IsStorefront)
                .IsUnique()
                .HasFilter("\"IsStorefront\" = TRUE");
        }
    }
}
