using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class productionseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                INSERT INTO "Role" ("Id", "CreatedAt", "Description", "IsDeleted", "RoleName")
                VALUES
                    (1, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Full access.', FALSE, 'Admin'),
                    (2, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Standard customer access.', FALSE, 'Customer')
                ON CONFLICT DO NOTHING;

                INSERT INTO "Categories" ("Id", "CategoryName", "CreatedAt", "IsDeleted")
                VALUES
                    (1, 'Telefon', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE),
                    (2, 'Bilgisayar ve Tablet', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE),
                    (3, 'Bilgisayar Bileşenleri ve Aksesuarları', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE),
                    (4, 'Ses Sistemleri', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE),
                    (5, 'TV ve Görüntü Sistemleri', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE),
                    (6, 'Kamera ve Fotoğraf', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE),
                    (7, 'Ev Aletleri', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE),
                    (8, 'Oyun ve Konsol', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE),
                    (9, 'Giyilebilir Teknoloji', TIMESTAMPTZ '2026-04-25T00:00:00Z', FALSE)
                ON CONFLICT DO NOTHING;

                INSERT INTO "Users" ("Id", "CreatedAt", "Email", "IsDeleted", "PasswordHash", "PhoneNumber", "RoleId", "Username")
                VALUES
                    (1, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'admin@gmail.com', FALSE, 'rJaJ4ickJwheNbnT4+i+2IyzQ0gotDuG/AWWytTG4nA=', '5550000001', 1, 'Admin'),
                    (2, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'hikmet@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000002', 2, 'Hikmet Kütük'),
                    (3, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'okan@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000003', 2, 'Okan Demirkan'),
                    (4, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'onur@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000004', 2, 'Onur Demir'),
                    (5, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'mustafa@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000005', 2, 'Mustafa Ardınç'),
                    (6, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'kurtulus@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000006', 2, 'Kurtuluş Tekeci'),
                    (7, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'samet@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000007', 2, 'Samet Can Bayraktar'),
                    (8, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'ebrar@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000008', 2, 'Ebar Karabacak'),
                    (9, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'tugra@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000009', 2, 'Tuğra Mert Nehir'),
                    (10, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'bayram@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000010', 2, 'Bayram Furkan Korkmaz'),
                    (11, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'mustafakarabacak@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000011', 2, 'Mustafa Karabacak'),
                    (12, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'osman@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000012', 2, 'Osman Korkmaz'),
                    (13, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'kodal@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000013', 2, 'Muhammet Kodal'),
                    (14, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'çetin@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000014', 2, 'Abdullah Çetin'),
                    (15, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'ismail@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000015', 2, 'İsmail Sercen Öztürk'),
                    (16, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'kemal@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000016', 2, 'Kemal Ayhan'),
                    (17, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'şükran@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000017', 2, 'Şükran Kayabaşıoğlu'),
                    (18, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'murat@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000018', 2, 'Murat Salihoğlu'),
                    (19, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'faik@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000019', 2, 'Faik Emre Pusat'),
                    (20, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'ömer@gmail.com', FALSE, 'gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=', '5550000020', 2, 'Ömer Saroğlu')
                ON CONFLICT DO NOTHING;

                INSERT INTO "Products" ("Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsDeleted", "Price", "ProductName", "Stock")
                VALUES
                    (1, 1, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1695048133142-1a20484d2569?w=600', FALSE, 54999.99, 'Apple iPhone 15 Pro', 50),
                    (2, 1, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://img.pzrmcdn.com/mnresize/716/716/asset/96a68f98-2d8f-409b-8dd3-15f659ff3e1a8806095303635/images/samsunggalaxys24ultragri512gb12gbramaklltelefonsamsungtrkiyegarantili-2.png', FALSE, 49999.99, 'Samsung Galaxy S24 Ultra', 40),
                    (3, 4, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1618366712010-f4ae9c647dcb?w=600', FALSE, 8999.99, 'Sony WH-1000XM5 Kulaklık', 75),
                    (4, 2, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=600', FALSE, 74999.99, 'Apple MacBook Pro 14"', 20),
                    (5, 3, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=600', FALSE, 2499.99, 'Logitech MX Master 3 Mouse', 120),
                    (6, 5, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=600', FALSE, 34999.99, 'Samsung 4K OLED TV 55"', 15),
                    (7, 2, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQu3UfWG0QSdCHCYuEgbmR1LNgZFTygbTLDB-VR_itLkoXoxQhQ-A2vWifu&s=10', FALSE, 42999.99, 'iPad Pro 12.9" M4', 30),
                    (8, 3, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://cdn.dsmcdn.com/mnresize/420/620/ty1029/product/media/images/prod/SPM/PIM/20231101/10/c424d82b-1811-38f2-8e19-928711cf23fb/1_org_zoom.jpg', FALSE, 1899.99, 'Mechanical Gaming Klavye', 90),
                    (9, 6, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=600', FALSE, 13999.99, 'GoPro Hero 12 Black', 35),
                    (10, 5, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=600', FALSE, 18499.99, 'Dell UltraSharp 27" Monitör', 25),
                    (11, 7, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1558317374-067fb5f30001?w=600', FALSE, 19999.99, 'Dyson V15 Detect Süpürge', 18),
                    (12, 6, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=600', FALSE, 64999.99, 'Nikon Z6 III Fotoğraf Makinesi', 12),
                    (13, 9, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1546868871-7041f2a55e12?w=600', FALSE, 14999.99, 'Apple Watch Series 9', 60),
                    (14, 7, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1585664811087-47f65abbad64?w=600', FALSE, 4299.99, 'Philips Airfryer XXL', 45),
                    (15, 8, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1606813907291-d86efa9b94db?w=600', FALSE, 24999.99, 'PlayStation 5 Slim', 22),
                    (16, 4, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=600', FALSE, 6999.99, 'Bose SoundLink Max Hoparlör', 55),
                    (17, 2, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=600', FALSE, 38999.99, 'LG Gram 16 Laptop', 28),
                    (18, 7, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1589894404892-7310b92ea7a2?w=600', FALSE, 9499.99, 'Xiaomi Robot Süpürge S20+', 33),
                    (19, 6, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1510127034890-ba27508e9f1c?w=600', FALSE, 29999.99, 'Canon EOS R50 Aynasız', 17),
                    (20, 3, TIMESTAMPTZ '2026-04-25T00:00:00Z', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.', 'https://images.unsplash.com/photo-1591799264318-7e6ef8ddb7ea?w=600', FALSE, 3799.99, 'Corsair RM1000x PSU', 42)
                ON CONFLICT DO NOTHING;
                """
            );

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "Price", "ProductName", "Stock" },
                values: new object[,]
                {
                    { 101, 1, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Günlük kullanım için güçlü kamera ve uzun pil ömrü sunan akıllı telefon.", "https://placehold.co/600x600/png?text=Galaxy+A55", 18999.99m, "Samsung Galaxy A55", 64 },
                    { 102, 1, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Yüksek yenileme hızlı ekran ve hızlı şarj destekli performans telefonu.", "https://placehold.co/600x600/png?text=Redmi+Note+13+Pro", 15999.99m, "Xiaomi Redmi Note 13 Pro", 72 },
                    { 103, 1, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "A15 Bionic işlemci, kaliteli kamera ve uzun yazılım desteğiyle iPhone deneyimi.", "https://placehold.co/600x600/png?text=iPhone+14", 37999.99m, "Apple iPhone 14", 35 },
                    { 104, 2, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Hafif gövde, verimli işlemci ve günlük işler için ideal dizüstü bilgisayar.", "https://placehold.co/600x600/png?text=IdeaPad+Slim+5", 28999.99m, "Lenovo IdeaPad Slim 5", 28 },
                    { 105, 2, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "OLED ekranlı, kompakt ve premium ultrabook deneyimi.", "https://placehold.co/600x600/png?text=Zenbook+14+OLED", 42999.99m, "Asus Zenbook 14 OLED", 18 },
                    { 106, 2, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Tablet esnekliği ve laptop gücünü bir araya getiren hibrit cihaz.", "https://placehold.co/600x600/png?text=Surface+Pro+10", 52999.99m, "Microsoft Surface Pro 10", 14 },
                    { 107, 3, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Oyun ve üretkenlik sistemleri için yüksek frekanslı DDR5 bellek kiti.", "https://placehold.co/600x600/png?text=Kingston+DDR5+RAM", 4299.99m, "Kingston Fury 32GB DDR5 RAM", 85 },
                    { 108, 3, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "PCIe 4.0 destekli yüksek hızlı NVMe depolama birimi.", "https://placehold.co/600x600/png?text=Samsung+990+Pro", 6999.99m, "Samsung 990 Pro 2TB SSD", 50 },
                    { 109, 3, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Yüksek FPS ve ray tracing performansı için güçlü ekran kartı.", "https://placehold.co/600x600/png?text=RTX+4070+Super", 32999.99m, "NVIDIA RTX 4070 Super", 16 },
                    { 110, 4, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Aktif gürültü engelleme ve uzun pil ömrü sunan kablosuz kulaklık.", "https://placehold.co/600x600/png?text=JBL+Tune+770NC", 3999.99m, "JBL Tune 770NC", 70 },
                    { 111, 4, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Kompakt tasarımlı, güçlü ses veren taşınabilir Bluetooth hoparlör.", "https://placehold.co/600x600/png?text=Marshall+Emberton+II", 5999.99m, "Marshall Emberton II", 38 },
                    { 112, 4, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Premium ses kalitesi ve gelişmiş gürültü engelleme özellikleri.", "https://placehold.co/600x600/png?text=Momentum+4", 11999.99m, "Sennheiser Momentum 4", 27 },
                    { 113, 5, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "OLED panel, düşük gecikme ve sinematik görüntü deneyimi.", "https://placehold.co/600x600/png?text=LG+OLED+C4", 69999.99m, "LG OLED C4 65\"", 9 },
                    { 114, 5, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Yüksek yenileme hızlı kavisli oyuncu monitörü.", "https://placehold.co/600x600/png?text=Odyssey+G7", 22999.99m, "Samsung Odyssey G7 32\"", 22 },
                    { 115, 5, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Televizyonlara 4K akıllı yayın özelliği kazandıran kompakt medya oynatıcı.", "https://placehold.co/600x600/png?text=Mi+TV+Stick+4K", 1999.99m, "Xiaomi Mi TV Stick 4K", 95 },
                    { 116, 6, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Hızlı otofokus ve kaliteli video özellikleriyle aynasız kamera.", "https://placehold.co/600x600/png?text=Sony+A6700", 55999.99m, "Sony Alpha A6700", 11 },
                    { 117, 6, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Aksiyon çekimleri için dayanıklı ve yüksek kaliteli kompakt kamera.", "https://placehold.co/600x600/png?text=DJI+Osmo+Action+4", 14999.99m, "DJI Osmo Action 4", 24 },
                    { 118, 6, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Portre ve günlük çekimler için hafif ve keskin prime lens.", "https://placehold.co/600x600/png?text=Canon+RF+50mm", 7999.99m, "Canon RF 50mm Lens", 31 },
                    { 119, 7, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Türk kahvesi hazırlamak için kompakt ve pratik kahve makinesi.", "https://placehold.co/600x600/png?text=Arzum+Okka", 3499.99m, "Arzum Okka Kahve Makinesi", 46 },
                    { 120, 7, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Az yağlı pişirme ve ızgara özelliği sunan sıcak hava fritözü.", "https://placehold.co/600x600/png?text=Tefal+Easy+Fry", 5499.99m, "Tefal Easy Fry Grill", 39 },
                    { 121, 7, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Güçlü emiş ve akıllı haritalama özelliğiyle robot süpürge.", "https://placehold.co/600x600/png?text=Roborock+Q8+Max", 18999.99m, "Roborock Q8 Max", 21 },
                    { 122, 8, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "4K oyun deneyimi ve hızlı yükleme süreleri sunan oyun konsolu.", "https://placehold.co/600x600/png?text=Xbox+Series+X", 27999.99m, "Xbox Series X", 19 },
                    { 123, 8, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Canlı OLED ekranlı, elde ve televizyonda oynanabilen oyun konsolu.", "https://placehold.co/600x600/png?text=Switch+OLED", 15999.99m, "Nintendo Switch OLED", 34 },
                    { 124, 8, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Profesyonel oyuncular için hafif kablosuz gaming mouse.", "https://placehold.co/600x600/png?text=G+Pro+X+Superlight", 4999.99m, "Logitech G Pro X Superlight", 58 },
                    { 125, 9, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Sağlık takip özellikleri ve uzun pil ömrü sunan akıllı saat.", "https://placehold.co/600x600/png?text=Garmin+Venu+3", 16999.99m, "Garmin Venu 3", 26 },
                    { 126, 9, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Şık tasarımlı, spor ve sağlık takip özellikli akıllı saat.", "https://placehold.co/600x600/png?text=Watch+GT+4", 8999.99m, "Huawei Watch GT 4", 44 },
                    { 127, 9, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Android telefonlarla uyumlu kapsamlı akıllı saat deneyimi.", "https://placehold.co/600x600/png?text=Galaxy+Watch+6", 7999.99m, "Samsung Galaxy Watch 6", 49 },
                    { 128, 3, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Yüksek kapasiteli, hızlı şarj destekli taşınabilir güç kaynağı.", "https://placehold.co/600x600/png?text=Anker+737", 4299.99m, "Anker 737 PowerBank", 67 },
                    { 129, 3, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Laptoplar için çoklu port ve monitör bağlantısı sunan USB C dock.", "https://placehold.co/600x600/png?text=Ugreen+USB+C+Dock", 2999.99m, "Ugreen USB C Dock", 53 },
                    { 130, 3, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "WiFi 6 destekli, yüksek kapsama alanına sahip kablosuz router.", "https://placehold.co/600x600/png?text=Archer+AX73", 3799.99m, "TP Link Archer AX73", 41 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[,]
                {
                    { 101, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ayse.yilmaz@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000101", 2, "Ayşe Yılmaz" },
                    { 102, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "mehmet.kaya@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000102", 2, "Mehmet Kaya" },
                    { 103, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "elif.demir@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000103", 2, "Elif Demir" },
                    { 104, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "burak.arslan@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000104", 2, "Burak Arslan" },
                    { 105, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "zeynep.sahin@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000105", 2, "Zeynep Şahin" },
                    { 106, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "emre.celik@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000106", 2, "Emre Çelik" },
                    { 107, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "derya.aydin@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000107", 2, "Derya Aydın" },
                    { 108, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "kerem.yildiz@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000108", 2, "Kerem Yıldız" },
                    { 109, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "selin.koc@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000109", 2, "Selin Koç" },
                    { 110, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "mert.aksoy@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000110", 2, "Mert Aksoy" },
                    { 111, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "cansu.eren@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000111", 2, "Cansu Eren" },
                    { 112, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "tolga.kurt@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000112", 2, "Tolga Kurt" },
                    { 113, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "nehir.ozkan@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000113", 2, "Nehir Özkan" },
                    { 114, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "can.yalcin@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000114", 2, "Can Yalçın" },
                    { 115, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ece.polat@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000115", 2, "Ece Polat" },
                    { 116, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "onur.aslan@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000116", 2, "Onur Aslan" },
                    { 117, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "seda.gunes@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000117", 2, "Seda Güneş" },
                    { 118, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "bora.kaplan@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000118", 2, "Bora Kaplan" },
                    { 119, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "mina.tas@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000119", 2, "Mina Taş" },
                    { 120, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ali.dogan@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000120", 2, "Ali Doğan" },
                    { 121, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "yagmur.deniz@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000121", 2, "Yağmur Deniz" },
                    { 122, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "kaan.ersoy@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000122", 2, "Kaan Ersoy" },
                    { 123, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "irem.sari@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000123", 2, "İrem Sarı" },
                    { 124, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "deniz.acar@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000124", 2, "Deniz Acar" },
                    { 125, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "pelin.bulut@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000125", 2, "Pelin Bulut" },
                    { 126, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "eren.korkmaz@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000126", 2, "Eren Korkmaz" },
                    { 127, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "melis.keskin@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000127", 2, "Melis Keskin" },
                    { 128, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "arda.tekin@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000128", 2, "Arda Tekin" },
                    { 129, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "nazli.cakir@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000129", 2, "Nazlı Çakır" },
                    { 130, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "tuna.yucel@example.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5551000130", 2, "Tuna Yücel" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressType", "City", "CreatedAt", "District", "FullAddress", "UserId", "ZipCode" },
                values: new object[,]
                {
                    { 301, "Home", "İstanbul", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Kadıköy", "Moda Caddesi No:12 Daire:4", 101, "34710" },
                    { 302, "Home", "Ankara", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Çankaya", "Atatürk Bulvarı No:48 Daire:9", 102, "06420" },
                    { 303, "Home", "İzmir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Karşıyaka", "Bahariye Sokak No:7 Daire:2", 103, "35550" },
                    { 304, "Home", "Bursa", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Nilüfer", "Cumhuriyet Mahallesi No:22 Daire:6", 104, "16140" },
                    { 305, "Home", "Antalya", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Muratpaşa", "Lara Caddesi No:5 Daire:11", 105, "07160" },
                    { 306, "Home", "Eskişehir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Tepebaşı", "Üniversite Caddesi No:31 Daire:3", 106, "26170" },
                    { 307, "Home", "Konya", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Selçuklu", "Yazır Mahallesi No:19 Daire:8", 107, "42250" },
                    { 308, "Home", "Kocaeli", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "İzmit", "Fethiye Caddesi No:3 Daire:5", 108, "41050" },
                    { 309, "Home", "Trabzon", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Ortahisar", "Uzun Sokak No:44 Daire:10", 109, "61030" },
                    { 310, "Home", "Adana", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Seyhan", "Ziyapaşa Bulvari No:27 Daire:7", 110, "01120" },
                    { 311, "Home", "Mersin", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Yenişehir", "Gazi Mustafa Kemal Bulvari No:8 Daire:5", 111, "33110" },
                    { 312, "Job", "Mersin", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Mezitli", "Viransehir Mahallesi Is Merkezi No:14 Kat:2", 111, "33340" },
                    { 313, "Home", "Kayseri", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Melikgazi", "Sivas Caddesi No:36 Daire:12", 112, "38030" },
                    { 314, "Other", "Kayseri", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Kocasinan", "Sümer Mahallesi No:18 Daire:1", 112, "38090" },
                    { 315, "Home", "Samsun", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Atakum", "Denizevleri Mahallesi No:11 Daire:6", 113, "55200" },
                    { 316, "Job", "Samsun", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Ilkadim", "Liman Mahallesi Ofis Plaza No:21 Kat:4", 113, "55060" },
                    { 317, "Home", "Balıkesir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Altıeylül", "Milli Kuvvetler Caddesi No:14 Daire:9", 114, "10100" },
                    { 318, "Other", "Balıkesir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Karesi", "Pasaalani Mahallesi No:24 Daire:2", 114, "10020" },
                    { 319, "Home", "Muğla", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Menteşe", "Cumhuriyet Sokak No:6 Daire:3", 115, "48000" },
                    { 320, "Job", "Muğla", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Ula", "Akyaka Yolu Ofis No:4", 115, "48650" },
                    { 321, "Home", "İstanbul", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Beşiktaş", "Abbasağa Mahallesi No:18 Daire:5", 116, "34353" },
                    { 322, "Job", "İstanbul", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Sisli", "Buyukdere Caddesi Plaza No:72 Kat:8", 116, "34394" },
                    { 323, "Home", "Ankara", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Keçiören", "Fatih Caddesi No:63 Daire:15", 117, "06280" },
                    { 324, "Other", "Ankara", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Yenimahalle", "Batiken Mahallesi No:34 Daire:4", 117, "06370" },
                    { 325, "Home", "İzmir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Bornova", "Kazım Dirik Mahallesi No:29 Daire:7", 118, "35100" },
                    { 326, "Job", "İzmir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Konak", "Cumhuriyet Bulvari Is Hani No:88 Kat:3", 118, "35210" },
                    { 327, "Home", "Bursa", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Osmangazi", "Altıparmak Caddesi No:41 Daire:6", 119, "16050" },
                    { 328, "Other", "Bursa", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Mudanya", "Guzelyali Mahallesi No:13 Daire:2", 119, "16940" },
                    { 329, "Home", "Antalya", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Konyaaltı", "Akdeniz Bulvari No:72 Daire:18", 120, "07070" },
                    { 330, "Job", "Antalya", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Kepez", "Gazi Bulvari Ofis No:52 Kat:5", 120, "07090" },
                    { 331, "Home", "Edirne", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Merkez", "Saraçlar Caddesi No:16 Daire:3", 121, "22020" },
                    { 332, "Job", "Edirne", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Merkez", "Talatpasa Caddesi Ofis No:25 Kat:2", 121, "22100" },
                    { 333, "Other", "Kirklareli", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Luleburgaz", "Yeni Mahalle No:10 Daire:4", 121, "39750" },
                    { 334, "Home", "Gaziantep", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Şahinbey", "İnönü Caddesi No:58 Daire:11", 122, "27010" },
                    { 335, "Job", "Gaziantep", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Sehitkamil", "Organize Sanayi Bolgesi No:42", 122, "27600" },
                    { 336, "Other", "Gaziantep", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Nizip", "Mimar Sinan Mahallesi No:19 Daire:2", 122, "27700" },
                    { 337, "Home", "Sakarya", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Serdivan", "Arabacıalanı No:23 Daire:7", 123, "54050" },
                    { 338, "Job", "Sakarya", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Adapazari", "Cark Caddesi Ofis No:31 Kat:1", 123, "54100" },
                    { 339, "Other", "Kocaeli", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Kartepe", "Maşukiye Mahallesi No:6", 123, "41295" },
                    { 340, "Home", "Tekirdağ", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Süleymanpaşa", "Hürriyet Mahallesi No:9 Daire:8", 124, "59030" },
                    { 341, "Job", "Tekirdağ", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Corlu", "Omurtak Caddesi Plaza No:44 Kat:6", 124, "59850" },
                    { 342, "Other", "Tekirdağ", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Marmaraereglisi", "Sahil Mahallesi No:12 Daire:1", 124, "59740" },
                    { 343, "Home", "Denizli", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Pamukkale", "Çamlık Caddesi No:32 Daire:10", 125, "20160" },
                    { 344, "Job", "Denizli", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Merkezefendi", "Gazi Bulvari Is Merkezi No:27 Kat:3", 125, "20010" },
                    { 345, "Other", "Aydın", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Kuşadası", "Marina Sokak No:5 Daire:2", 125, "09400" },
                    { 346, "Home", "İstanbul", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Uskudar", "Baglarbasi Caddesi No:21 Daire:4", 126, "34664" },
                    { 347, "Job", "İstanbul", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Kadıköy", "Rasim Pasa Mahallesi Ofis No:17 Kat:2", 126, "34716" },
                    { 348, "Other", "Yalova", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Merkez", "Çiftlikköy Yolu No:8 Daire:1", 126, "77100" },
                    { 349, "Home", "Ankara", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Mamak", "Dostlar Mahallesi No:47 Daire:6", 127, "06630" },
                    { 350, "Job", "Ankara", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Çankaya", "Tunalı Hilmi Caddesi Ofis No:36 Kat:5", 127, "06680" },
                    { 351, "Other", "Eskişehir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Odunpazarı", "Atatürk Bulvarı No:19 Daire:3", 127, "26020" },
                    { 352, "Home", "İzmir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Balçova", "Ata Caddesi No:25 Daire:9", 128, "35330" },
                    { 353, "Job", "İzmir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Bayraklı", "Manas Bulvari Tower No:39 Kat:11", 128, "35530" },
                    { 354, "Other", "Manisa", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Yunusemre", "Laleli Mahallesi No:16 Daire:5", 128, "45030" },
                    { 355, "Home", "Bursa", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Yıldırım", "Davutdede Mahallesi No:28 Daire:7", 129, "16350" },
                    { 356, "Job", "Bursa", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Nilüfer", "Görükle Teknopark No:12 Kat:2", 129, "16285" },
                    { 357, "Other", "Balıkesir", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Edremit", "Akçay Mahallesi No:9 Daire:1", 129, "10300" },
                    { 358, "Home", "Antalya", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Alanya", "Oba Mahallesi No:31 Daire:12", 130, "07460" },
                    { 359, "Job", "Antalya", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Muratpaşa", "Işıklar Caddesi Ofis No:18 Kat:4", 130, "07100" },
                    { 360, "Other", "Isparta", new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Merkez", "Bahçelievler Mahallesi No:22 Daire:2", 130, "32200" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "OrderStatus", "ShippingAddress", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1001, new DateTime(2026, 7, 1, 10, 15, 0, 0, DateTimeKind.Utc), false, "Delivered", "İstanbul, Kadıköy, Moda Caddesi No:12", 34999.98m, 101 },
                    { 1002, new DateTime(2026, 7, 1, 14, 25, 0, 0, DateTimeKind.Utc), false, "Shipped", "Ankara, Çankaya, Atatürk Bulvarı No:48", 28999.99m, 102 },
                    { 1003, new DateTime(2026, 7, 2, 9, 0, 0, 0, DateTimeKind.Utc), false, "Pending", "İzmir, Karşıyaka, Bahariye Sokak No:7", 11899.98m, 103 },
                    { 1004, new DateTime(2026, 7, 2, 16, 40, 0, 0, DateTimeKind.Utc), false, "Delivered", "Bursa, Nilüfer, Cumhuriyet Mahallesi No:22", 37999.99m, 104 },
                    { 1005, new DateTime(2026, 7, 3, 11, 10, 0, 0, DateTimeKind.Utc), false, "Canceled", "Antalya, Muratpaşa, Lara Caddesi No:5", 48999.98m, 105 },
                    { 1006, new DateTime(2026, 7, 3, 18, 5, 0, 0, DateTimeKind.Utc), false, "Delivered", "Eskişehir, Tepebaşı, Üniversite Caddesi No:31", 4299.99m, 106 },
                    { 1007, new DateTime(2026, 7, 4, 8, 45, 0, 0, DateTimeKind.Utc), false, "Shipped", "Konya, Selçuklu, Yazır Mahallesi No:19", 75999.98m, 107 },
                    { 1008, new DateTime(2026, 7, 4, 13, 20, 0, 0, DateTimeKind.Utc), false, "Pending", "Kocaeli, İzmit, Fethiye Caddesi No:3", 14999.99m, 108 },
                    { 1009, new DateTime(2026, 7, 4, 20, 35, 0, 0, DateTimeKind.Utc), false, "Delivered", "Trabzon, Ortahisar, Uzun Sokak No:44", 20999.96m, 109 },
                    { 1010, new DateTime(2026, 7, 5, 10, 5, 0, 0, DateTimeKind.Utc), false, "Shipped", "Adana, Seyhan, Ziyapaşa Bulvarı No:27", 55999.99m, 110 },
                    { 1011, new DateTime(2026, 7, 5, 12, 50, 0, 0, DateTimeKind.Utc), false, "Pending", "Mersin, Yenişehir, Gazi Mustafa Kemal Bulvarı No:8", 15999.99m, 111 },
                    { 1012, new DateTime(2026, 7, 5, 17, 30, 0, 0, DateTimeKind.Utc), false, "Delivered", "Kayseri, Melikgazi, Sivas Caddesi No:36", 28499.98m, 112 },
                    { 1013, new DateTime(2026, 7, 6, 9, 45, 0, 0, DateTimeKind.Utc), false, "Canceled", "Samsun, Atakum, Denizevleri Mahallesi No:11", 18999.99m, 113 },
                    { 1014, new DateTime(2026, 7, 6, 15, 15, 0, 0, DateTimeKind.Utc), false, "Delivered", "Balıkesir, Altıeylül, Milli Kuvvetler Caddesi No:14", 12499.97m, 114 },
                    { 1015, new DateTime(2026, 7, 6, 19, 0, 0, 0, DateTimeKind.Utc), false, "Shipped", "Muğla, Menteşe, Cumhuriyet Sokak No:6", 69999.99m, 115 },
                    { 1016, new DateTime(2026, 7, 7, 8, 10, 0, 0, DateTimeKind.Utc), false, "Pending", "İstanbul, Beşiktaş, Abbasağa Mahallesi No:18", 52999.99m, 116 },
                    { 1017, new DateTime(2026, 7, 7, 10, 35, 0, 0, DateTimeKind.Utc), false, "Delivered", "Ankara, Keçiören, Fatih Caddesi No:63", 70999.98m, 117 },
                    { 1018, new DateTime(2026, 7, 7, 12, 5, 0, 0, DateTimeKind.Utc), false, "Shipped", "İzmir, Bornova, Kazım Dirik Mahallesi No:29", 27999.99m, 118 },
                    { 1019, new DateTime(2026, 7, 7, 13, 55, 0, 0, DateTimeKind.Utc), false, "Pending", "Bursa, Osmangazi, Altıparmak Caddesi No:41", 7999.99m, 119 },
                    { 1020, new DateTime(2026, 7, 7, 15, 45, 0, 0, DateTimeKind.Utc), false, "Delivered", "Antalya, Konyaaltı, Akdeniz Bulvarı No:72", 48999.98m, 120 },
                    { 1021, new DateTime(2026, 7, 8, 9, 25, 0, 0, DateTimeKind.Utc), false, "Shipped", "Edirne, Merkez, Saraçlar Caddesi No:16", 14799.97m, 121 },
                    { 1022, new DateTime(2026, 7, 8, 11, 40, 0, 0, DateTimeKind.Utc), false, "Pending", "Gaziantep, Şahinbey, İnönü Caddesi No:58", 11999.98m, 122 },
                    { 1023, new DateTime(2026, 7, 8, 14, 10, 0, 0, DateTimeKind.Utc), false, "Delivered", "Sakarya, Serdivan, Arabacıalanı No:23", 42999.99m, 123 },
                    { 1024, new DateTime(2026, 7, 8, 16, 35, 0, 0, DateTimeKind.Utc), false, "Canceled", "Tekirdağ, Süleymanpaşa, Hürriyet Mahallesi No:9", 22099.96m, 124 },
                    { 1025, new DateTime(2026, 7, 8, 18, 55, 0, 0, DateTimeKind.Utc), false, "Pending", "Denizli, Pamukkale, Çamlık Caddesi No:32", 42999.99m, 125 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "ImageUrl", "IsDeleted", "OrderId", "Price", "ProductId", "ProductName", "Quantity" },
                values: new object[,]
                {
                    { 2001, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Galaxy+A55", false, 1001, 18999.99m, 101, "Samsung Galaxy A55", 1 },
                    { 2002, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Switch+OLED", false, 1001, 15999.99m, 123, "Nintendo Switch OLED", 1 },
                    { 2003, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=IdeaPad+Slim+5", false, 1002, 28999.99m, 104, "Lenovo IdeaPad Slim 5", 1 },
                    { 2004, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=JBL+Tune+770NC", false, 1003, 3999.99m, 110, "JBL Tune 770NC", 1 },
                    { 2005, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Canon+RF+50mm", false, 1003, 7999.99m, 118, "Canon RF 50mm Lens", 1 },
                    { 2006, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=iPhone+14", false, 1004, 37999.99m, 103, "Apple iPhone 14", 1 },
                    { 2007, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Redmi+Note+13+Pro", false, 1005, 15999.99m, 102, "Xiaomi Redmi Note 13 Pro", 1 },
                    { 2008, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=RTX+4070+Super", false, 1005, 32999.99m, 109, "NVIDIA RTX 4070 Super", 1 },
                    { 2009, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Anker+737", false, 1006, 4299.99m, 128, "Anker 737 PowerBank", 1 },
                    { 2010, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Zenbook+14+OLED", false, 1007, 42999.99m, 105, "Asus Zenbook 14 OLED", 1 },
                    { 2011, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=RTX+4070+Super", false, 1007, 32999.99m, 109, "NVIDIA RTX 4070 Super", 1 },
                    { 2012, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=DJI+Osmo+Action+4", false, 1008, 14999.99m, 117, "DJI Osmo Action 4", 1 },
                    { 2013, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Arzum+Okka", false, 1009, 3499.99m, 119, "Arzum Okka Kahve Makinesi", 2 },
                    { 2014, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Samsung+990+Pro", false, 1009, 6999.99m, 108, "Samsung 990 Pro 2TB SSD", 2 },
                    { 2015, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Sony+A6700", false, 1010, 55999.99m, 116, "Sony Alpha A6700", 1 },
                    { 2016, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Switch+OLED", false, 1011, 15999.99m, 123, "Nintendo Switch OLED", 1 },
                    { 2017, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Tefal+Easy+Fry", false, 1012, 5499.99m, 120, "Tefal Easy Fry Grill", 1 },
                    { 2018, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Odyssey+G7", false, 1012, 22999.99m, 114, "Samsung Odyssey G7 32\"", 1 },
                    { 2019, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Roborock+Q8+Max", false, 1013, 18999.99m, 121, "Roborock Q8 Max", 1 },
                    { 2020, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Ugreen+USB+C+Dock", false, 1014, 2999.99m, 129, "Ugreen USB C Dock", 1 },
                    { 2021, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=G+Pro+X+Superlight", false, 1014, 4999.99m, 124, "Logitech G Pro X Superlight", 1 },
                    { 2022, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Tefal+Easy+Fry", false, 1014, 5499.99m, 120, "Tefal Easy Fry Grill", 1 },
                    { 2023, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=LG+OLED+C4", false, 1015, 69999.99m, 113, "LG OLED C4 65\"", 1 },
                    { 2024, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Surface+Pro+10", false, 1016, 52999.99m, 106, "Microsoft Surface Pro 10", 1 },
                    { 2025, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Xbox+Series+X", false, 1017, 27999.99m, 122, "Xbox Series X", 1 },
                    { 2026, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Zenbook+14+OLED", false, 1017, 42999.99m, 105, "Asus Zenbook 14 OLED", 1 },
                    { 2027, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Xbox+Series+X", false, 1018, 27999.99m, 122, "Xbox Series X", 1 },
                    { 2028, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Galaxy+Watch+6", false, 1019, 7999.99m, 127, "Samsung Galaxy Watch 6", 1 },
                    { 2029, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Marshall+Emberton+II", false, 1020, 5999.99m, 111, "Marshall Emberton II", 1 },
                    { 2030, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Zenbook+14+OLED", false, 1020, 42999.99m, 105, "Asus Zenbook 14 OLED", 1 },
                    { 2031, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Arzum+Okka", false, 1021, 3499.99m, 119, "Arzum Okka Kahve Makinesi", 1 },
                    { 2032, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Samsung+990+Pro", false, 1021, 6999.99m, 108, "Samsung 990 Pro 2TB SSD", 1 },
                    { 2033, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Anker+737", false, 1021, 4299.99m, 128, "Anker 737 PowerBank", 1 },
                    { 2034, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Marshall+Emberton+II", false, 1022, 5999.99m, 111, "Marshall Emberton II", 2 },
                    { 2035, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Zenbook+14+OLED", false, 1023, 42999.99m, 105, "Asus Zenbook 14 OLED", 1 },
                    { 2036, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Archer+AX73", false, 1024, 3799.99m, 130, "TP Link Archer AX73", 1 },
                    { 2037, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Watch+GT+4", false, 1024, 8999.99m, 126, "Huawei Watch GT 4", 1 },
                    { 2038, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=G+Pro+X+Superlight", false, 1024, 4999.99m, 124, "Logitech G Pro X Superlight", 1 },
                    { 2039, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Kingston+DDR5+RAM", false, 1024, 4299.99m, 107, "Kingston Fury 32GB DDR5 RAM", 1 },
                    { 2040, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/600x600/png?text=Zenbook+14+OLED", false, 1025, 42999.99m, 105, "Asus Zenbook 14 OLED", 1 }
                });

            migrationBuilder.Sql(
                """
                SELECT setval(pg_get_serial_sequence('"Role"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Role"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"Categories"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Categories"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"Users"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Users"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"Products"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Products"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"Addresses"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Addresses"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"Orders"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Orders"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"OrderItems"', 'Id'), COALESCE((SELECT MAX("Id") FROM "OrderItems"), 1), TRUE);
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2002);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2003);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2004);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2005);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2006);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2007);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2008);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2009);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2010);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2011);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2012);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2013);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2014);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2015);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2016);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2017);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2018);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2019);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2020);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2021);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2022);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2023);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2024);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2025);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2026);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2027);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2028);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2029);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2030);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2031);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2032);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2033);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2034);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2035);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2036);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2037);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2038);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2039);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2040);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 125);
        }
    }
}
