using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductionUserSeedConfiguration : IEntityTypeConfiguration<User>
    {
        private const string DefaultPasswordHash = "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=";
        private static readonly DateTime SeedDate = new(2026, 7, 7, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new { Id = 101, Username = "Ayşe Yılmaz", Email = "ayse.yilmaz@example.com", PhoneNumber = "5551000101", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 102, Username = "Mehmet Kaya", Email = "mehmet.kaya@example.com", PhoneNumber = "5551000102", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 103, Username = "Elif Demir", Email = "elif.demir@example.com", PhoneNumber = "5551000103", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 104, Username = "Burak Arslan", Email = "burak.arslan@example.com", PhoneNumber = "5551000104", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 105, Username = "Zeynep Şahin", Email = "zeynep.sahin@example.com", PhoneNumber = "5551000105", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 106, Username = "Emre Çelik", Email = "emre.celik@example.com", PhoneNumber = "5551000106", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 107, Username = "Derya Aydın", Email = "derya.aydin@example.com", PhoneNumber = "5551000107", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 108, Username = "Kerem Yıldız", Email = "kerem.yildiz@example.com", PhoneNumber = "5551000108", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 109, Username = "Selin Koç", Email = "selin.koc@example.com", PhoneNumber = "5551000109", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 110, Username = "Mert Aksoy", Email = "mert.aksoy@example.com", PhoneNumber = "5551000110", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 111, Username = "Cansu Eren", Email = "cansu.eren@example.com", PhoneNumber = "5551000111", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 112, Username = "Tolga Kurt", Email = "tolga.kurt@example.com", PhoneNumber = "5551000112", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 113, Username = "Nehir Özkan", Email = "nehir.ozkan@example.com", PhoneNumber = "5551000113", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 114, Username = "Can Yalçın", Email = "can.yalcin@example.com", PhoneNumber = "5551000114", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 115, Username = "Ece Polat", Email = "ece.polat@example.com", PhoneNumber = "5551000115", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 116, Username = "Onur Aslan", Email = "onur.aslan@example.com", PhoneNumber = "5551000116", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 117, Username = "Seda Güneş", Email = "seda.gunes@example.com", PhoneNumber = "5551000117", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 118, Username = "Bora Kaplan", Email = "bora.kaplan@example.com", PhoneNumber = "5551000118", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 119, Username = "Mina Taş", Email = "mina.tas@example.com", PhoneNumber = "5551000119", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 120, Username = "Ali Doğan", Email = "ali.dogan@example.com", PhoneNumber = "5551000120", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 121, Username = "Yağmur Deniz", Email = "yagmur.deniz@example.com", PhoneNumber = "5551000121", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 122, Username = "Kaan Ersoy", Email = "kaan.ersoy@example.com", PhoneNumber = "5551000122", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 123, Username = "İrem Sarı", Email = "irem.sari@example.com", PhoneNumber = "5551000123", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 124, Username = "Deniz Acar", Email = "deniz.acar@example.com", PhoneNumber = "5551000124", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 125, Username = "Pelin Bulut", Email = "pelin.bulut@example.com", PhoneNumber = "5551000125", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 126, Username = "Eren Korkmaz", Email = "eren.korkmaz@example.com", PhoneNumber = "5551000126", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 127, Username = "Melis Keskin", Email = "melis.keskin@example.com", PhoneNumber = "5551000127", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 128, Username = "Arda Tekin", Email = "arda.tekin@example.com", PhoneNumber = "5551000128", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 129, Username = "Nazlı Çakır", Email = "nazli.cakir@example.com", PhoneNumber = "5551000129", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 130, Username = "Tuna Yücel", Email = "tuna.yucel@example.com", PhoneNumber = "5551000130", PasswordHash = DefaultPasswordHash, RoleId = 2, CreatedAt = SeedDate, IsDeleted = false }
            );
        }
    }

    public class ProductionProductSeedConfiguration : IEntityTypeConfiguration<Product>
    {
        private static readonly DateTime SeedDate = new(2026, 7, 7, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new { Id = 101, ProductName = "Samsung Galaxy A55", Description = "Günlük kullanım için güçlü kamera ve uzun pil ömrü sunan akıllı telefon.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrOGQG59sIYnGFf2asWcaoZ4oQjSfpfN2AS6u4YTbAjA&s=10", Price = 18999.99m, Stock = 64, CategoryId = 1, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 102, ProductName = "Xiaomi Redmi Note 13 Pro", Description = "Yüksek yenileme hızlı ekran ve hızlı şarj destekli performans telefonu.", ImageUrl = "https://i02.appmifile.com/426_item_tr/25/03/2024/81ca5eae9adeb0cf494a38e7e85631c9.png", Price = 15999.99m, Stock = 72, CategoryId = 1, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 103, ProductName = "Apple iPhone 14", Description = "A15 Bionic işlemci, kaliteli kamera ve uzun yazılım desteğiyle iPhone deneyimi.", ImageUrl = "https://cdn.cimri.io/image/1000x1000/apple-iphone-14-128gb_933352424.jpg", Price = 37999.99m, Stock = 35, CategoryId = 1, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 104, ProductName = "Lenovo IdeaPad Slim 5", Description = "Hafif gövde, verimli işlemci ve günlük işler için ideal dizüstü bilgisayar.", ImageUrl = "https://placehold.co/600x600/png?text=IdeaPad+Slim+5", Price = 28999.99m, Stock = 28, CategoryId = 2, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 105, ProductName = "Asus Zenbook 14 OLED", Description = "OLED ekranlı, kompakt ve premium ultrabook deneyimi.", ImageUrl = "https://static.ticimax.cloud/cdn-cgi/image/width=-,quality=99/77825/uploads/urunresimleri/buyuk/lenovo-ideapad-slim-5-83hr0049tr-i5-13-e3-4c6.jpg", Price = 42999.99m, Stock = 18, CategoryId = 2, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 106, ProductName = "Microsoft Surface Pro 10", Description = "Tablet esnekliği ve laptop gücünü bir araya getiren hibrit cihaz.", ImageUrl = "https://www.fgee.co.ke/wp-content/uploads/2025/07/microsoft-surface1.png", Price = 52999.99m, Stock = 14, CategoryId = 2, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 107, ProductName = "Kingston Fury 32GB DDR5 RAM", Description = "Oyun ve üretkenlik sistemleri için yüksek frekanslı DDR5 bellek kiti.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQzQwrPguBA5VwZcjW6HPzOewwJMU46SmXYi73GtiFCgQ&s", Price = 4299.99m, Stock = 85, CategoryId = 3, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 108, ProductName = "Samsung 990 Pro 2TB SSD", Description = "PCIe 4.0 destekli yüksek hızlı NVMe depolama birimi.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAZLZS7Coesj2C4Czx41_W94JYG7_TV0XyjqBiDR97t5ukNqDztImt7vI&s=10", Price = 6999.99m, Stock = 50, CategoryId = 3, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 109, ProductName = "NVIDIA RTX 4070 Super", Description = "Yüksek FPS ve ray tracing performansı için güçlü ekran kartı.", ImageUrl = "https://www.tebilon.com/picture/msi-geforcertx-4070-super-12g-gaming-x-slim-12gb-gddr6x-192bit-nvidia-dlss-3-ekran-karti.jpg", Price = 32999.99m, Stock = 16, CategoryId = 3, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 110, ProductName = "JBL Tune 770NC", Description = "Aktif gürültü engelleme ve uzun pil ömrü sunan kablosuz kulaklık.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbynmvr4yR7Fkf7kEdJHfeX7388pHYnxv8NJPktz43VQ&s", Price = 3999.99m, Stock = 70, CategoryId = 4, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 111, ProductName = "Marshall Emberton II", Description = "Kompakt tasarımlı, güçlü ses veren taşınabilir Bluetooth hoparlör.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTGUtusZoWWMQMgQItVygJUvQ_U8RlN11ijyY05c0LMOw&s", Price = 5999.99m, Stock = 38, CategoryId = 4, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 112, ProductName = "Sennheiser Momentum 4", Description = "Premium ses kalitesi ve gelişmiş gürültü engelleme özellikleri.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQC3Sat-ryNBR0HEkBzU1Tp6YYnXmpIHu0UN1Mp2rs1Hg&s=10", Price = 11999.99m, Stock = 27, CategoryId = 4, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 113, ProductName = "LG OLED C4 65\"", Description = "OLED panel, düşük gecikme ve sinematik görüntü deneyimi.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRwOYdR6ZTSSvbtYGEOczCo7n_b24aNQMPC7lX5w3Vsd_Bw9P_gGslRcVpY&s=10", Price = 69999.99m, Stock = 9, CategoryId = 5, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 114, ProductName = "Samsung Odyssey G7 32\"", Description = "Yüksek yenileme hızlı kavisli oyuncu monitörü.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQmecMBCS6NxcRGLtn_Z65KcPktBIMYUXe-vVYlSvPE5g&s=10", Price = 22999.99m, Stock = 22, CategoryId = 5, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 115, ProductName = "Xiaomi Mi TV Stick 4K", Description = "Televizyonlara 4K akıllı yayın özelliği kazandıran kompakt medya oynatıcı.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR1S0ZDlclunerW6ZN81SFL485uU9yvxEmuvXffLVqA-g&s=10", Price = 1999.99m, Stock = 95, CategoryId = 5, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 116, ProductName = "Sony Alpha A6700", Description = "Hızlı otofokus ve kaliteli video özellikleriyle aynasız kamera.", ImageUrl = "https://cdn.vatanbilgisayar.com/Upload/PRODUCT/sony/thumb/142034-0_large.jpg", Price = 55999.99m, Stock = 11, CategoryId = 6, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 117, ProductName = "DJI Osmo Action 4", Description = "Aksiyon çekimleri için dayanıklı ve yüksek kaliteli kompakt kamera.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR9-L2bYRGf9hr4XaWtOfZA4IUUJsEUZT_sC4vUj3jG75ZN7uUN9rcqwF5D&s=10", Price = 14999.99m, Stock = 24, CategoryId = 6, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 118, ProductName = "Canon RF 50mm Lens", Description = "Portre ve günlük çekimler için hafif ve keskin prime lens.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScIs4p-U7OETqU6ZZ_ZsJh8CZHiR3Z5W4u5uESvxpSSw&s", Price = 7999.99m, Stock = 31, CategoryId = 6, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 119, ProductName = "Arzum Okka Kahve Makinesi", Description = "Türk kahvesi hazırlamak için kompakt ve pratik kahve makinesi.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdAFapQQwcxO_d_5VhcVJ5Dnc3RdExa8txvQ1NapSqYfbN8QrAuzn9wrwb&s=10", Price = 3499.99m, Stock = 46, CategoryId = 7, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 120, ProductName = "Tefal Easy Fry Grill", Description = "Az yağlı pişirme ve ızgara özelliği sunan sıcak hava fritözü.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTx1N4OC5MygwwcJDS4Tr5xOTlOKxYjKmyJbOS-5_9nesnBTQFH_jcNvDM&s=10", Price = 5499.99m, Stock = 39, CategoryId = 7, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 121, ProductName = "Roborock Q8 Max", Description = "Güçlü emiş ve akıllı haritalama özelliğiyle robot süpürge.", ImageUrl = "https://cdn.akakce.com/z/roborock/roborock-q8-max-akilli.jpg", Price = 18999.99m, Stock = 21, CategoryId = 7, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 122, ProductName = "Xbox Series X", Description = "4K oyun deneyimi ve hızlı yükleme süreleri sunan oyun konsolu.", ImageUrl = "https://cdn.dsmcdn.com/mnresize/420/620/ty1630/prod/QC/20250128/15/7ec69eae-a327-3a3b-a819-fa8fe56f5974/1_org_zoom.jpg", Price = 27999.99m, Stock = 19, CategoryId = 8, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 123, ProductName = "Nintendo Switch OLED", Description = "Canlı OLED ekranlı, elde ve televizyonda oynanabilen oyun konsolu.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVnvOXIQqLVpJ0ZG6nkoYcGVUcBlHE-THVXfAoIUrUoA&s=10", Price = 15999.99m, Stock = 34, CategoryId = 8, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 124, ProductName = "Logitech G Pro X Superlight", Description = "Profesyonel oyuncular için hafif kablosuz gaming mouse.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRBzRdDyZT65sMOgO0MWjaLTvXCkxTB1FRKWR83gWWOaJNqeekoKPi_Na0g&s=10", Price = 4999.99m, Stock = 58, CategoryId = 8, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 125, ProductName = "Garmin Venu 3", Description = "Sağlık takip özellikleri ve uzun pil ömrü sunan akıllı saat.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRZpSOC1TRP3ceT3U-WDgLTG3FoAX-KUb5bKXhfYhEMMA&s=10", Price = 16999.99m, Stock = 26, CategoryId = 9, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 126, ProductName = "Huawei Watch GT 4", Description = "Şık tasarımlı, spor ve sağlık takip özellikli akıllı saat.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSygvqZi7wzqsyHJyGkI9pUSQAB4rwhwGGUbSaaCElcQu4Ef9VJFuOg6_mn&s=10", Price = 8999.99m, Stock = 44, CategoryId = 9, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 127, ProductName = "Samsung Galaxy Watch 6", Description = "Android telefonlarla uyumlu kapsamlı akıllı saat deneyimi.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRsMb6vj_TXbPKgzZl1dhIWgfq2pJxsWFQXUOGsDfQyQA&s=10", Price = 7999.99m, Stock = 49, CategoryId = 9, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 128, ProductName = "Anker 737 PowerBank", Description = "Yüksek kapasiteli, hızlı şarj destekli taşınabilir güç kaynağı.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSWHXGpB3b6T2Ch9mp1vJOG9TgvjVTidBWboijGr7czoDgwkHbUy9hLVc4&s=10", Price = 4299.99m, Stock = 67, CategoryId = 3, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 129, ProductName = "Ugreen USB C Dock", Description = "Laptoplar için çoklu port ve monitör bağlantısı sunan USB C dock.", ImageUrl = "https://m.media-amazon.com/images/I/71FVxWx4lNL._AC_UF1000,1000_QL80_.jpg", Price = 2999.99m, Stock = 53, CategoryId = 3, IsDeleted = false, CreatedAt = SeedDate },
                new { Id = 130, ProductName = "TP Link Archer AX73", Description = "WiFi 6 destekli, yüksek kapsama alanına sahip kablosuz router.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTP8UcK1jYU2wrfaQ3X7TiWCCe1LtYwia_H9SX1kKTG4UTnZCOgQ7ebmCc&s=10", Price = 3799.99m, Stock = 41, CategoryId = 3, IsDeleted = false, CreatedAt = SeedDate }
            );
        }
    }

    public class ProductionAddressSeedConfiguration : IEntityTypeConfiguration<Address>
    {
        private static readonly DateTime SeedDate = new(2026, 7, 7, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(
                new { Id = 301, UserId = 101, AddressType = AddressType.Home, City = "İstanbul", District = "Kadıköy", FullAddress = "Moda Caddesi No:12 Daire:4", ZipCode = "34710", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 302, UserId = 102, AddressType = AddressType.Home, City = "Ankara", District = "Çankaya", FullAddress = "Atatürk Bulvarı No:48 Daire:9", ZipCode = "06420", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 303, UserId = 103, AddressType = AddressType.Home, City = "İzmir", District = "Karşıyaka", FullAddress = "Bahariye Sokak No:7 Daire:2", ZipCode = "35550", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 304, UserId = 104, AddressType = AddressType.Home, City = "Bursa", District = "Nilüfer", FullAddress = "Cumhuriyet Mahallesi No:22 Daire:6", ZipCode = "16140", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 305, UserId = 105, AddressType = AddressType.Home, City = "Antalya", District = "Muratpaşa", FullAddress = "Lara Caddesi No:5 Daire:11", ZipCode = "07160", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 306, UserId = 106, AddressType = AddressType.Home, City = "Eskişehir", District = "Tepebaşı", FullAddress = "Üniversite Caddesi No:31 Daire:3", ZipCode = "26170", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 307, UserId = 107, AddressType = AddressType.Home, City = "Konya", District = "Selçuklu", FullAddress = "Yazır Mahallesi No:19 Daire:8", ZipCode = "42250", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 308, UserId = 108, AddressType = AddressType.Home, City = "Kocaeli", District = "İzmit", FullAddress = "Fethiye Caddesi No:3 Daire:5", ZipCode = "41050", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 309, UserId = 109, AddressType = AddressType.Home, City = "Trabzon", District = "Ortahisar", FullAddress = "Uzun Sokak No:44 Daire:10", ZipCode = "61030", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 310, UserId = 110, AddressType = AddressType.Home, City = "Adana", District = "Seyhan", FullAddress = "Ziyapaşa Bulvari No:27 Daire:7", ZipCode = "01120", CreatedAt = SeedDate, IsDeleted = false },

                new { Id = 311, UserId = 111, AddressType = AddressType.Home, City = "Mersin", District = "Yenişehir", FullAddress = "Gazi Mustafa Kemal Bulvari No:8 Daire:5", ZipCode = "33110", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 312, UserId = 111, AddressType = AddressType.Job, City = "Mersin", District = "Mezitli", FullAddress = "Viransehir Mahallesi Is Merkezi No:14 Kat:2", ZipCode = "33340", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 313, UserId = 112, AddressType = AddressType.Home, City = "Kayseri", District = "Melikgazi", FullAddress = "Sivas Caddesi No:36 Daire:12", ZipCode = "38030", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 314, UserId = 112, AddressType = AddressType.Other, City = "Kayseri", District = "Kocasinan", FullAddress = "Sümer Mahallesi No:18 Daire:1", ZipCode = "38090", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 315, UserId = 113, AddressType = AddressType.Home, City = "Samsun", District = "Atakum", FullAddress = "Denizevleri Mahallesi No:11 Daire:6", ZipCode = "55200", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 316, UserId = 113, AddressType = AddressType.Job, City = "Samsun", District = "Ilkadim", FullAddress = "Liman Mahallesi Ofis Plaza No:21 Kat:4", ZipCode = "55060", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 317, UserId = 114, AddressType = AddressType.Home, City = "Balıkesir", District = "Altıeylül", FullAddress = "Milli Kuvvetler Caddesi No:14 Daire:9", ZipCode = "10100", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 318, UserId = 114, AddressType = AddressType.Other, City = "Balıkesir", District = "Karesi", FullAddress = "Pasaalani Mahallesi No:24 Daire:2", ZipCode = "10020", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 319, UserId = 115, AddressType = AddressType.Home, City = "Muğla", District = "Menteşe", FullAddress = "Cumhuriyet Sokak No:6 Daire:3", ZipCode = "48000", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 320, UserId = 115, AddressType = AddressType.Job, City = "Muğla", District = "Ula", FullAddress = "Akyaka Yolu Ofis No:4", ZipCode = "48650", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 321, UserId = 116, AddressType = AddressType.Home, City = "İstanbul", District = "Beşiktaş", FullAddress = "Abbasağa Mahallesi No:18 Daire:5", ZipCode = "34353", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 322, UserId = 116, AddressType = AddressType.Job, City = "İstanbul", District = "Sisli", FullAddress = "Buyukdere Caddesi Plaza No:72 Kat:8", ZipCode = "34394", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 323, UserId = 117, AddressType = AddressType.Home, City = "Ankara", District = "Keçiören", FullAddress = "Fatih Caddesi No:63 Daire:15", ZipCode = "06280", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 324, UserId = 117, AddressType = AddressType.Other, City = "Ankara", District = "Yenimahalle", FullAddress = "Batiken Mahallesi No:34 Daire:4", ZipCode = "06370", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 325, UserId = 118, AddressType = AddressType.Home, City = "İzmir", District = "Bornova", FullAddress = "Kazım Dirik Mahallesi No:29 Daire:7", ZipCode = "35100", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 326, UserId = 118, AddressType = AddressType.Job, City = "İzmir", District = "Konak", FullAddress = "Cumhuriyet Bulvari Is Hani No:88 Kat:3", ZipCode = "35210", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 327, UserId = 119, AddressType = AddressType.Home, City = "Bursa", District = "Osmangazi", FullAddress = "Altıparmak Caddesi No:41 Daire:6", ZipCode = "16050", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 328, UserId = 119, AddressType = AddressType.Other, City = "Bursa", District = "Mudanya", FullAddress = "Guzelyali Mahallesi No:13 Daire:2", ZipCode = "16940", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 329, UserId = 120, AddressType = AddressType.Home, City = "Antalya", District = "Konyaaltı", FullAddress = "Akdeniz Bulvari No:72 Daire:18", ZipCode = "07070", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 330, UserId = 120, AddressType = AddressType.Job, City = "Antalya", District = "Kepez", FullAddress = "Gazi Bulvari Ofis No:52 Kat:5", ZipCode = "07090", CreatedAt = SeedDate, IsDeleted = false },

                new { Id = 331, UserId = 121, AddressType = AddressType.Home, City = "Edirne", District = "Merkez", FullAddress = "Saraçlar Caddesi No:16 Daire:3", ZipCode = "22020", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 332, UserId = 121, AddressType = AddressType.Job, City = "Edirne", District = "Merkez", FullAddress = "Talatpasa Caddesi Ofis No:25 Kat:2", ZipCode = "22100", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 333, UserId = 121, AddressType = AddressType.Other, City = "Kirklareli", District = "Luleburgaz", FullAddress = "Yeni Mahalle No:10 Daire:4", ZipCode = "39750", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 334, UserId = 122, AddressType = AddressType.Home, City = "Gaziantep", District = "Şahinbey", FullAddress = "İnönü Caddesi No:58 Daire:11", ZipCode = "27010", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 335, UserId = 122, AddressType = AddressType.Job, City = "Gaziantep", District = "Sehitkamil", FullAddress = "Organize Sanayi Bolgesi No:42", ZipCode = "27600", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 336, UserId = 122, AddressType = AddressType.Other, City = "Gaziantep", District = "Nizip", FullAddress = "Mimar Sinan Mahallesi No:19 Daire:2", ZipCode = "27700", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 337, UserId = 123, AddressType = AddressType.Home, City = "Sakarya", District = "Serdivan", FullAddress = "Arabacıalanı No:23 Daire:7", ZipCode = "54050", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 338, UserId = 123, AddressType = AddressType.Job, City = "Sakarya", District = "Adapazari", FullAddress = "Cark Caddesi Ofis No:31 Kat:1", ZipCode = "54100", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 339, UserId = 123, AddressType = AddressType.Other, City = "Kocaeli", District = "Kartepe", FullAddress = "Maşukiye Mahallesi No:6", ZipCode = "41295", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 340, UserId = 124, AddressType = AddressType.Home, City = "Tekirdağ", District = "Süleymanpaşa", FullAddress = "Hürriyet Mahallesi No:9 Daire:8", ZipCode = "59030", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 341, UserId = 124, AddressType = AddressType.Job, City = "Tekirdağ", District = "Corlu", FullAddress = "Omurtak Caddesi Plaza No:44 Kat:6", ZipCode = "59850", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 342, UserId = 124, AddressType = AddressType.Other, City = "Tekirdağ", District = "Marmaraereglisi", FullAddress = "Sahil Mahallesi No:12 Daire:1", ZipCode = "59740", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 343, UserId = 125, AddressType = AddressType.Home, City = "Denizli", District = "Pamukkale", FullAddress = "Çamlık Caddesi No:32 Daire:10", ZipCode = "20160", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 344, UserId = 125, AddressType = AddressType.Job, City = "Denizli", District = "Merkezefendi", FullAddress = "Gazi Bulvari Is Merkezi No:27 Kat:3", ZipCode = "20010", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 345, UserId = 125, AddressType = AddressType.Other, City = "Aydın", District = "Kuşadası", FullAddress = "Marina Sokak No:5 Daire:2", ZipCode = "09400", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 346, UserId = 126, AddressType = AddressType.Home, City = "İstanbul", District = "Uskudar", FullAddress = "Baglarbasi Caddesi No:21 Daire:4", ZipCode = "34664", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 347, UserId = 126, AddressType = AddressType.Job, City = "İstanbul", District = "Kadıköy", FullAddress = "Rasim Pasa Mahallesi Ofis No:17 Kat:2", ZipCode = "34716", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 348, UserId = 126, AddressType = AddressType.Other, City = "Yalova", District = "Merkez", FullAddress = "Çiftlikköy Yolu No:8 Daire:1", ZipCode = "77100", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 349, UserId = 127, AddressType = AddressType.Home, City = "Ankara", District = "Mamak", FullAddress = "Dostlar Mahallesi No:47 Daire:6", ZipCode = "06630", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 350, UserId = 127, AddressType = AddressType.Job, City = "Ankara", District = "Çankaya", FullAddress = "Tunalı Hilmi Caddesi Ofis No:36 Kat:5", ZipCode = "06680", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 351, UserId = 127, AddressType = AddressType.Other, City = "Eskişehir", District = "Odunpazarı", FullAddress = "Atatürk Bulvarı No:19 Daire:3", ZipCode = "26020", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 352, UserId = 128, AddressType = AddressType.Home, City = "İzmir", District = "Balçova", FullAddress = "Ata Caddesi No:25 Daire:9", ZipCode = "35330", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 353, UserId = 128, AddressType = AddressType.Job, City = "İzmir", District = "Bayraklı", FullAddress = "Manas Bulvari Tower No:39 Kat:11", ZipCode = "35530", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 354, UserId = 128, AddressType = AddressType.Other, City = "Manisa", District = "Yunusemre", FullAddress = "Laleli Mahallesi No:16 Daire:5", ZipCode = "45030", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 355, UserId = 129, AddressType = AddressType.Home, City = "Bursa", District = "Yıldırım", FullAddress = "Davutdede Mahallesi No:28 Daire:7", ZipCode = "16350", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 356, UserId = 129, AddressType = AddressType.Job, City = "Bursa", District = "Nilüfer", FullAddress = "Görükle Teknopark No:12 Kat:2", ZipCode = "16285", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 357, UserId = 129, AddressType = AddressType.Other, City = "Balıkesir", District = "Edremit", FullAddress = "Akçay Mahallesi No:9 Daire:1", ZipCode = "10300", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 358, UserId = 130, AddressType = AddressType.Home, City = "Antalya", District = "Alanya", FullAddress = "Oba Mahallesi No:31 Daire:12", ZipCode = "07460", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 359, UserId = 130, AddressType = AddressType.Job, City = "Antalya", District = "Muratpaşa", FullAddress = "Işıklar Caddesi Ofis No:18 Kat:4", ZipCode = "07100", CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 360, UserId = 130, AddressType = AddressType.Other, City = "Isparta", District = "Merkez", FullAddress = "Bahçelievler Mahallesi No:22 Daire:2", ZipCode = "32200", CreatedAt = SeedDate, IsDeleted = false }
            );
        }
    }

    public class ProductionOrderSeedConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(
                new { Id = 1001, UserId = 101, ShippingAddress = "İstanbul, Kadıköy, Moda Caddesi No:12", TotalPrice = 34999.98m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 1, 10, 15, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1002, UserId = 102, ShippingAddress = "Ankara, Çankaya, Atatürk Bulvarı No:48", TotalPrice = 28999.99m, OrderStatus = OrderStatus.Shipped, CreatedAt = new DateTime(2026, 7, 1, 14, 25, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1003, UserId = 103, ShippingAddress = "İzmir, Karşıyaka, Bahariye Sokak No:7", TotalPrice = 11899.98m, OrderStatus = OrderStatus.Pending, CreatedAt = new DateTime(2026, 7, 2, 9, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1004, UserId = 104, ShippingAddress = "Bursa, Nilüfer, Cumhuriyet Mahallesi No:22", TotalPrice = 37999.99m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 2, 16, 40, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1005, UserId = 105, ShippingAddress = "Antalya, Muratpaşa, Lara Caddesi No:5", TotalPrice = 48999.98m, OrderStatus = OrderStatus.Canceled, CreatedAt = new DateTime(2026, 7, 3, 11, 10, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1006, UserId = 106, ShippingAddress = "Eskişehir, Tepebaşı, Üniversite Caddesi No:31", TotalPrice = 4299.99m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 3, 18, 5, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1007, UserId = 107, ShippingAddress = "Konya, Selçuklu, Yazır Mahallesi No:19", TotalPrice = 75999.98m, OrderStatus = OrderStatus.Shipped, CreatedAt = new DateTime(2026, 7, 4, 8, 45, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1008, UserId = 108, ShippingAddress = "Kocaeli, İzmit, Fethiye Caddesi No:3", TotalPrice = 14999.99m, OrderStatus = OrderStatus.Pending, CreatedAt = new DateTime(2026, 7, 4, 13, 20, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1009, UserId = 109, ShippingAddress = "Trabzon, Ortahisar, Uzun Sokak No:44", TotalPrice = 20999.96m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 4, 20, 35, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1010, UserId = 110, ShippingAddress = "Adana, Seyhan, Ziyapaşa Bulvarı No:27", TotalPrice = 55999.99m, OrderStatus = OrderStatus.Shipped, CreatedAt = new DateTime(2026, 7, 5, 10, 5, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1011, UserId = 111, ShippingAddress = "Mersin, Yenişehir, Gazi Mustafa Kemal Bulvarı No:8", TotalPrice = 15999.99m, OrderStatus = OrderStatus.Pending, CreatedAt = new DateTime(2026, 7, 5, 12, 50, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1012, UserId = 112, ShippingAddress = "Kayseri, Melikgazi, Sivas Caddesi No:36", TotalPrice = 28499.98m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 5, 17, 30, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1013, UserId = 113, ShippingAddress = "Samsun, Atakum, Denizevleri Mahallesi No:11", TotalPrice = 18999.99m, OrderStatus = OrderStatus.Canceled, CreatedAt = new DateTime(2026, 7, 6, 9, 45, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1014, UserId = 114, ShippingAddress = "Balıkesir, Altıeylül, Milli Kuvvetler Caddesi No:14", TotalPrice = 12499.97m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 6, 15, 15, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1015, UserId = 115, ShippingAddress = "Muğla, Menteşe, Cumhuriyet Sokak No:6", TotalPrice = 69999.99m, OrderStatus = OrderStatus.Shipped, CreatedAt = new DateTime(2026, 7, 6, 19, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1016, UserId = 116, ShippingAddress = "İstanbul, Beşiktaş, Abbasağa Mahallesi No:18", TotalPrice = 52999.99m, OrderStatus = OrderStatus.Pending, CreatedAt = new DateTime(2026, 7, 7, 8, 10, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1017, UserId = 117, ShippingAddress = "Ankara, Keçiören, Fatih Caddesi No:63", TotalPrice = 70999.98m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 7, 10, 35, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1018, UserId = 118, ShippingAddress = "İzmir, Bornova, Kazım Dirik Mahallesi No:29", TotalPrice = 27999.99m, OrderStatus = OrderStatus.Shipped, CreatedAt = new DateTime(2026, 7, 7, 12, 5, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1019, UserId = 119, ShippingAddress = "Bursa, Osmangazi, Altıparmak Caddesi No:41", TotalPrice = 7999.99m, OrderStatus = OrderStatus.Pending, CreatedAt = new DateTime(2026, 7, 7, 13, 55, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1020, UserId = 120, ShippingAddress = "Antalya, Konyaaltı, Akdeniz Bulvarı No:72", TotalPrice = 48999.98m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 7, 15, 45, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1021, UserId = 121, ShippingAddress = "Edirne, Merkez, Saraçlar Caddesi No:16", TotalPrice = 14799.97m, OrderStatus = OrderStatus.Shipped, CreatedAt = new DateTime(2026, 7, 8, 9, 25, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1022, UserId = 122, ShippingAddress = "Gaziantep, Şahinbey, İnönü Caddesi No:58", TotalPrice = 11999.98m, OrderStatus = OrderStatus.Pending, CreatedAt = new DateTime(2026, 7, 8, 11, 40, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1023, UserId = 123, ShippingAddress = "Sakarya, Serdivan, Arabacıalanı No:23", TotalPrice = 42999.99m, OrderStatus = OrderStatus.Delivered, CreatedAt = new DateTime(2026, 7, 8, 14, 10, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1024, UserId = 124, ShippingAddress = "Tekirdağ, Süleymanpaşa, Hürriyet Mahallesi No:9", TotalPrice = 22099.96m, OrderStatus = OrderStatus.Canceled, CreatedAt = new DateTime(2026, 7, 8, 16, 35, 0, DateTimeKind.Utc), IsDeleted = false },
                new { Id = 1025, UserId = 125, ShippingAddress = "Denizli, Pamukkale, Çamlık Caddesi No:32", TotalPrice = 42999.99m, OrderStatus = OrderStatus.Pending, CreatedAt = new DateTime(2026, 7, 8, 18, 55, 0, DateTimeKind.Utc), IsDeleted = false }
            );
        }
    }

    public class ProductionOrderItemSeedConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        private static readonly DateTime SeedDate = new(2026, 7, 7, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasData(
                new { Id = 2001, OrderId = 1001, ProductId = 101, ProductName = "Samsung Galaxy A55", ImageUrl = "https://placehold.co/600x600/png?text=Galaxy+A55", Price = 18999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2002, OrderId = 1001, ProductId = 123, ProductName = "Nintendo Switch OLED", ImageUrl = "https://placehold.co/600x600/png?text=Switch+OLED", Price = 15999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2003, OrderId = 1002, ProductId = 104, ProductName = "Lenovo IdeaPad Slim 5", ImageUrl = "https://placehold.co/600x600/png?text=IdeaPad+Slim+5", Price = 28999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2004, OrderId = 1003, ProductId = 110, ProductName = "JBL Tune 770NC", ImageUrl = "https://placehold.co/600x600/png?text=JBL+Tune+770NC", Price = 3999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2005, OrderId = 1003, ProductId = 118, ProductName = "Canon RF 50mm Lens", ImageUrl = "https://placehold.co/600x600/png?text=Canon+RF+50mm", Price = 7999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2006, OrderId = 1004, ProductId = 103, ProductName = "Apple iPhone 14", ImageUrl = "https://placehold.co/600x600/png?text=iPhone+14", Price = 37999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2007, OrderId = 1005, ProductId = 102, ProductName = "Xiaomi Redmi Note 13 Pro", ImageUrl = "https://placehold.co/600x600/png?text=Redmi+Note+13+Pro", Price = 15999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2008, OrderId = 1005, ProductId = 109, ProductName = "NVIDIA RTX 4070 Super", ImageUrl = "https://placehold.co/600x600/png?text=RTX+4070+Super", Price = 32999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2009, OrderId = 1006, ProductId = 128, ProductName = "Anker 737 PowerBank", ImageUrl = "https://placehold.co/600x600/png?text=Anker+737", Price = 4299.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2010, OrderId = 1007, ProductId = 105, ProductName = "Asus Zenbook 14 OLED", ImageUrl = "https://placehold.co/600x600/png?text=Zenbook+14+OLED", Price = 42999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2011, OrderId = 1007, ProductId = 109, ProductName = "NVIDIA RTX 4070 Super", ImageUrl = "https://placehold.co/600x600/png?text=RTX+4070+Super", Price = 32999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2012, OrderId = 1008, ProductId = 117, ProductName = "DJI Osmo Action 4", ImageUrl = "https://placehold.co/600x600/png?text=DJI+Osmo+Action+4", Price = 14999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2013, OrderId = 1009, ProductId = 119, ProductName = "Arzum Okka Kahve Makinesi", ImageUrl = "https://placehold.co/600x600/png?text=Arzum+Okka", Price = 3499.99m, Quantity = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2014, OrderId = 1009, ProductId = 108, ProductName = "Samsung 990 Pro 2TB SSD", ImageUrl = "https://placehold.co/600x600/png?text=Samsung+990+Pro", Price = 6999.99m, Quantity = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2015, OrderId = 1010, ProductId = 116, ProductName = "Sony Alpha A6700", ImageUrl = "https://placehold.co/600x600/png?text=Sony+A6700", Price = 55999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2016, OrderId = 1011, ProductId = 123, ProductName = "Nintendo Switch OLED", ImageUrl = "https://placehold.co/600x600/png?text=Switch+OLED", Price = 15999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2017, OrderId = 1012, ProductId = 120, ProductName = "Tefal Easy Fry Grill", ImageUrl = "https://placehold.co/600x600/png?text=Tefal+Easy+Fry", Price = 5499.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2018, OrderId = 1012, ProductId = 114, ProductName = "Samsung Odyssey G7 32\"", ImageUrl = "https://placehold.co/600x600/png?text=Odyssey+G7", Price = 22999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2019, OrderId = 1013, ProductId = 121, ProductName = "Roborock Q8 Max", ImageUrl = "https://placehold.co/600x600/png?text=Roborock+Q8+Max", Price = 18999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2020, OrderId = 1014, ProductId = 129, ProductName = "Ugreen USB C Dock", ImageUrl = "https://placehold.co/600x600/png?text=Ugreen+USB+C+Dock", Price = 2999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2021, OrderId = 1014, ProductId = 124, ProductName = "Logitech G Pro X Superlight", ImageUrl = "https://placehold.co/600x600/png?text=G+Pro+X+Superlight", Price = 4999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2022, OrderId = 1014, ProductId = 120, ProductName = "Tefal Easy Fry Grill", ImageUrl = "https://placehold.co/600x600/png?text=Tefal+Easy+Fry", Price = 5499.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2023, OrderId = 1015, ProductId = 113, ProductName = "LG OLED C4 65\"", ImageUrl = "https://placehold.co/600x600/png?text=LG+OLED+C4", Price = 69999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2024, OrderId = 1016, ProductId = 106, ProductName = "Microsoft Surface Pro 10", ImageUrl = "https://placehold.co/600x600/png?text=Surface+Pro+10", Price = 52999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2025, OrderId = 1017, ProductId = 122, ProductName = "Xbox Series X", ImageUrl = "https://placehold.co/600x600/png?text=Xbox+Series+X", Price = 27999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2026, OrderId = 1017, ProductId = 105, ProductName = "Asus Zenbook 14 OLED", ImageUrl = "https://placehold.co/600x600/png?text=Zenbook+14+OLED", Price = 42999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2027, OrderId = 1018, ProductId = 122, ProductName = "Xbox Series X", ImageUrl = "https://placehold.co/600x600/png?text=Xbox+Series+X", Price = 27999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2028, OrderId = 1019, ProductId = 127, ProductName = "Samsung Galaxy Watch 6", ImageUrl = "https://placehold.co/600x600/png?text=Galaxy+Watch+6", Price = 7999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2029, OrderId = 1020, ProductId = 111, ProductName = "Marshall Emberton II", ImageUrl = "https://placehold.co/600x600/png?text=Marshall+Emberton+II", Price = 5999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2030, OrderId = 1020, ProductId = 105, ProductName = "Asus Zenbook 14 OLED", ImageUrl = "https://placehold.co/600x600/png?text=Zenbook+14+OLED", Price = 42999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2031, OrderId = 1021, ProductId = 119, ProductName = "Arzum Okka Kahve Makinesi", ImageUrl = "https://placehold.co/600x600/png?text=Arzum+Okka", Price = 3499.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2032, OrderId = 1021, ProductId = 108, ProductName = "Samsung 990 Pro 2TB SSD", ImageUrl = "https://placehold.co/600x600/png?text=Samsung+990+Pro", Price = 6999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2033, OrderId = 1021, ProductId = 128, ProductName = "Anker 737 PowerBank", ImageUrl = "https://placehold.co/600x600/png?text=Anker+737", Price = 4299.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2034, OrderId = 1022, ProductId = 111, ProductName = "Marshall Emberton II", ImageUrl = "https://placehold.co/600x600/png?text=Marshall+Emberton+II", Price = 5999.99m, Quantity = 2, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2035, OrderId = 1023, ProductId = 105, ProductName = "Asus Zenbook 14 OLED", ImageUrl = "https://placehold.co/600x600/png?text=Zenbook+14+OLED", Price = 42999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2036, OrderId = 1024, ProductId = 130, ProductName = "TP Link Archer AX73", ImageUrl = "https://placehold.co/600x600/png?text=Archer+AX73", Price = 3799.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2037, OrderId = 1024, ProductId = 126, ProductName = "Huawei Watch GT 4", ImageUrl = "https://placehold.co/600x600/png?text=Watch+GT+4", Price = 8999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2038, OrderId = 1024, ProductId = 124, ProductName = "Logitech G Pro X Superlight", ImageUrl = "https://placehold.co/600x600/png?text=G+Pro+X+Superlight", Price = 4999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2039, OrderId = 1024, ProductId = 107, ProductName = "Kingston Fury 32GB DDR5 RAM", ImageUrl = "https://placehold.co/600x600/png?text=Kingston+DDR5+RAM", Price = 4299.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 2040, OrderId = 1025, ProductId = 105, ProductName = "Asus Zenbook 14 OLED", ImageUrl = "https://placehold.co/600x600/png?text=Zenbook+14+OLED", Price = 42999.99m, Quantity = 1, CreatedAt = SeedDate, IsDeleted = false }
            );
        }
    }

    public class ProductionReviewSeedConfiguration : IEntityTypeConfiguration<Review>
    {
        private static readonly DateTime SeedDate = new(2026, 7, 7, 0, 0, 0, DateTimeKind.Utc);

        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(
                new { Id = 3001, UserId = 2, ProductId = 1, Comment = "Kamera performansı ve ekran kalitesi beklentimin üstünde.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3002, UserId = 3, ProductId = 2, Comment = (string?)null, Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3003, UserId = 4, ProductId = 3, Comment = "Gürültü engellemesi başarılı, uzun kullanımda da rahat.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3004, UserId = 5, ProductId = 4, Comment = "Performansı iyi, özellikle iş akışında oldukça hızlı.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3005, UserId = 6, ProductId = 5, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3006, UserId = 7, ProductId = 6, Comment = "Görüntü kalitesi çok canlı, salon için ideal oldu.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3007, UserId = 8, ProductId = 7, Comment = "Ekranı ve kalem deneyimi çok iyi.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3008, UserId = 9, ProductId = 8, Comment = (string?)null, Rating = (byte)3, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3009, UserId = 10, ProductId = 9, Comment = "Aksiyon çekimlerinde görüntü sabitlemesi başarılı.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3010, UserId = 11, ProductId = 10, Comment = "Renk doğruluğu iyi, tasarım işleri için memnun kaldım.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3011, UserId = 12, ProductId = 11, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3012, UserId = 13, ProductId = 12, Comment = "Fotoğraf kalitesi güçlü, gövde ergonomisi de iyi.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3013, UserId = 14, ProductId = 13, Comment = "Pil süresi ve sağlık takibi gayet başarılı.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3014, UserId = 15, ProductId = 14, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3015, UserId = 16, ProductId = 15, Comment = "Kurulumu kolay, oyun performansı beklediğim gibi.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3016, UserId = 17, ProductId = 16, Comment = "Ses seviyesi güçlü, baslar temiz geliyor.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3017, UserId = 18, ProductId = 17, Comment = (string?)null, Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3018, UserId = 19, ProductId = 18, Comment = "Haritalama düzgün, günlük temizlik için yeterli.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3019, UserId = 20, ProductId = 19, Comment = "Yeni başlayanlar için pratik ve kaliteli bir gövde.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3020, UserId = 101, ProductId = 20, Comment = (string?)null, Rating = (byte)3, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3021, UserId = 101, ProductId = 101, Comment = "Günlük kullanımda hızlı, ekran parlaklığı yeterli.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3022, UserId = 102, ProductId = 102, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3023, UserId = 103, ProductId = 103, Comment = "Kamera ve yazılım deneyimi istikrarlı.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3024, UserId = 104, ProductId = 104, Comment = "Hafif ve sessiz çalışıyor, ofis işleri için ideal.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3025, UserId = 105, ProductId = 105, Comment = (string?)null, Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3026, UserId = 106, ProductId = 106, Comment = "Tablet ve laptop arasında iyi bir denge sunuyor.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3027, UserId = 107, ProductId = 107, Comment = "Sistem yükseltmesi sonrası performans farkı hissediliyor.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3028, UserId = 108, ProductId = 108, Comment = (string?)null, Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3029, UserId = 109, ProductId = 109, Comment = "Oyunlarda yüksek ayarlarda akıcı sonuç aldım.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3030, UserId = 110, ProductId = 110, Comment = "Kulaklık rahat, bağlantısı stabil.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3031, UserId = 111, ProductId = 111, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3032, UserId = 112, ProductId = 112, Comment = "Ses kalitesi temiz, pil süresi tatmin edici.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3033, UserId = 113, ProductId = 113, Comment = "Siyah seviyesi ve kontrast gerçekten etkileyici.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3034, UserId = 114, ProductId = 114, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3035, UserId = 115, ProductId = 115, Comment = "Eski televizyonu pratik şekilde akıllı hale getirdi.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3036, UserId = 116, ProductId = 116, Comment = "Otofokus hızı ve video kalitesi güçlü.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3037, UserId = 117, ProductId = 117, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3038, UserId = 118, ProductId = 118, Comment = "Portre çekimleri için keskin ve hafif bir lens.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3039, UserId = 119, ProductId = 119, Comment = "Kahveyi hızlı hazırlıyor, temizliği kolay.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3040, UserId = 120, ProductId = 120, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3041, UserId = 121, ProductId = 121, Comment = "Emiş gücü iyi, uygulama üzerinden yönetimi kolay.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3042, UserId = 122, ProductId = 122, Comment = "Yükleme süreleri kısa, oyun deneyimi akıcı.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3043, UserId = 123, ProductId = 123, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3044, UserId = 124, ProductId = 124, Comment = "Çok hafif, hassasiyeti oyunlarda başarılı.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3045, UserId = 125, ProductId = 125, Comment = "Spor takibi detaylı, pil süresi iyi.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3046, UserId = 126, ProductId = 126, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3047, UserId = 127, ProductId = 127, Comment = "Telefonla uyumu sorunsuz, ekranı parlak.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3048, UserId = 128, ProductId = 128, Comment = "Kapasitesi yüksek, hızlı şarj desteği iş görüyor.", Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3049, UserId = 129, ProductId = 129, Comment = (string?)null, Rating = (byte)4, CreatedAt = SeedDate, IsDeleted = false },
                new { Id = 3050, UserId = 130, ProductId = 130, Comment = "Kapsama alanı güçlü, kurulum kısa sürdü.", Rating = (byte)5, CreatedAt = SeedDate, IsDeleted = false }
            );
        }
    }

}
