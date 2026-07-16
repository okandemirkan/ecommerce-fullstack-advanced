using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private const string DefaultPasswordHash = "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=";
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.Addresses)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.HasOne(c => c.Role)
                .WithMany()
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Restrict); //Role silinmeye çalışılırsa hata fırlatır.

            builder.Property(c => c.Username).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(70);
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(30);
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.PasswordHash).IsRequired().HasMaxLength(300);

            builder.HasIndex(c => new { c.WorkspaceId, c.Email }).IsUnique();
            builder.HasIndex(c => new { c.WorkspaceId, c.PhoneNumber }).IsUnique();
            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasData(
                new
                {
                    Id = 1,
                    Username = "Admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "5550000001",
                    PasswordHash = "rJaJ4ickJwheNbnT4+i+2IyzQ0gotDuG/AWWytTG4nA=",
                    RoleId = 1,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 2,
                    Username = "Hikmet Kütük",
                    Email = "hikmet@gmail.com",
                    PhoneNumber = "5550000002",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 3,
                    Username = "Okan Demirkan",
                    Email = "okan@gmail.com",
                    PhoneNumber = "5550000003",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 4,
                    Username = "Onur Demir",
                    Email = "onur@gmail.com",
                    PhoneNumber = "5550000004",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 5,
                    Username = "Mustafa Ardınç",
                    Email = "mustafa@gmail.com",
                    PhoneNumber = "5550000005",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 6,
                    Username = "Kurtuluş Tekeci",
                    Email = "kurtulus@gmail.com",
                    PhoneNumber = "5550000006",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 7,
                    Username = "Samet Can Bayraktar",
                    Email = "samet@gmail.com",
                    PhoneNumber = "5550000007",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 8,
                    Username = "Ebar Karabacak",
                    Email = "ebrar@gmail.com",
                    PhoneNumber = "5550000008",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 9,
                    Username = "Tuğra Mert Nehir",
                    Email = "tugra@gmail.com",
                    PhoneNumber = "5550000009",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 10,
                    Username = "Bayram Furkan Korkmaz",
                    Email = "bayram@gmail.com",
                    PhoneNumber = "5550000010",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 11,
                    Username = "Mustafa Karabacak",
                    Email = "mustafakarabacak@gmail.com",
                    PhoneNumber = "5550000011",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 12,
                    Username = "Osman Korkmaz",
                    Email = "osman@gmail.com",
                    PhoneNumber = "5550000012",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 13,
                    Username = "Muhammet Kodal",
                    Email = "kodal@gmail.com",
                    PhoneNumber = "5550000013",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 14,
                    Username = "Abdullah Çetin",
                    Email = "çetin@gmail.com",
                    PhoneNumber = "5550000014",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 15,
                    Username = "İsmail Sercen Öztürk",
                    Email = "ismail@gmail.com",
                    PhoneNumber = "5550000015",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 16,
                    Username = "Kemal Ayhan",
                    Email = "kemal@gmail.com",
                    PhoneNumber = "5550000016",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 17,
                    Username = "Şükran Kayabaşıoğlu",
                    Email = "şükran@gmail.com",
                    PhoneNumber = "5550000017",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 18,
                    Username = "Murat Salihoğlu",
                    Email = "murat@gmail.com",
                    PhoneNumber = "5550000018",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 19,
                    Username = "Faik Emre Pusat",
                    Email = "faik@gmail.com",
                    PhoneNumber = "5550000019",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new
                {
                    Id = 20,
                    Username = "Ömer Saroğlu",
                    Email = "ömer@gmail.com",
                    PhoneNumber = "5550000020",
                    PasswordHash = DefaultPasswordHash,
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
                );
        }
    }
}
