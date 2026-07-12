using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.User)
                .WithMany(a => a.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.City).IsRequired().HasMaxLength(40);
            builder.Property(a => a.AddressType).HasConversion<string>().IsRequired();    
            builder.Property(a => a.District).IsRequired().HasMaxLength(40);
            builder.Property(a => a.FullAddress).IsRequired().HasMaxLength(150);
            builder.Property(a => a.ZipCode).HasMaxLength(10);
            builder.Property(a => a.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.HasQueryFilter(a => !a.IsDeleted);
            
        }
    }
}
