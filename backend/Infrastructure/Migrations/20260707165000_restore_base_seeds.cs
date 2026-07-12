using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20260707165000_restore_base_seeds")]
    public partial class restore_base_seeds : Migration
    {
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

                SELECT setval(pg_get_serial_sequence('"Role"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Role"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"Users"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Users"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"Categories"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Categories"), 1), TRUE);
                SELECT setval(pg_get_serial_sequence('"Products"', 'Id'), COALESCE((SELECT MAX("Id") FROM "Products"), 1), TRUE);
                """
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
