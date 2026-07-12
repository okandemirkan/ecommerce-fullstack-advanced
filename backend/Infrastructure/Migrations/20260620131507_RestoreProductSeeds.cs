using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RestoreProductSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO "Products"
                (
                    "Id",
                    "ProductName",
                    "Description",
                    "ImageUrl",
                    "Price",
                    "Stock",
                    "CategoryId",
                    "CreatedAt",
                    "IsDeleted"
                )
                VALUES
                (
                    1,
                    'Apple iPhone 15 Pro',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1695048133142-1a20484d2569?w=600',
                    54999.99,
                    50,
                    1,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    2,
                    'Samsung Galaxy S24 Ultra',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1706439136251-58636289fc12?w=600',
                    49999.99,
                    40,
                    1,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    3,
                    'Sony WH-1000XM5 Kulaklık',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1618366712010-f4ae9c647dcb?w=600',
                    8999.99,
                    75,
                    4,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    4,
                    'Apple MacBook Pro 14"',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=600',
                    74999.99,
                    20,
                    2,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    5,
                    'Logitech MX Master 3 Mouse',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=600',
                    2499.99,
                    120,
                    3,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    6,
                    'Samsung 4K OLED TV 55"',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=600',
                    34999.99,
                    15,
                    5,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    7,
                    'iPad Pro 12.9" M4',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1544244015-0df43ffc6b0?w=600',
                    42999.99,
                    30,
                    2,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    8,
                    'Mechanical Gaming Klavye',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1541140532154-b02484d2569?w=600',
                    1899.99,
                    90,
                    3,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    9,
                    'GoPro Hero 12 Black',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=600',
                    13999.99,
                    35,
                    6,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    10,
                    'Dell UltraSharp 27" Monitör',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=600',
                    18499.99,
                    25,
                    5,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    11,
                    'Dyson V15 Detect Süpürge',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1558317374-067fb5f30001?w=600',
                    19999.99,
                    18,
                    7,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    12,
                    'Nikon Z6 III Fotoğraf Makinesi',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=600',
                    64999.99,
                    12,
                    6,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    13,
                    'Apple Watch Series 9',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1546868871-7041f2a55e12?w=600',
                    14999.99,
                    60,
                    9,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    14,
                    'Philips Airfryer XXL',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1585664811087-47f65abbad64?w=600',
                    4299.99,
                    45,
                    7,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    15,
                    'PlayStation 5 Slim',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1606813907291-d86efa9b94db?w=600',
                    24999.99,
                    22,
                    8,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    16,
                    'Bose SoundLink Max Hoparlör',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=600',
                    6999.99,
                    55,
                    4,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    17,
                    'LG Gram 16 Laptop',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=600',
                    38999.99,
                    28,
                    2,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    18,
                    'Xiaomi Robot Süpürge S20+',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1589894404892-7310b92ea7a2?w=600',
                    9499.99,
                    33,
                    7,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    19,
                    'Canon EOS R50 Aynasız',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1510127034890-ba27508e9f1c?w=600',
                    29999.99,
                    17,
                    6,
                    '2026-04-25 00:00:00+00',
                    false
                ),
                (
                    20,
                    'Corsair RM1000x PSU',
                    'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
                    'https://images.unsplash.com/photo-1591799264318-7e6ef8ddb7ea?w=600',
                    3799.99,
                    42,
                    3,
                    '2026-04-25 00:00:00+00',
                    false
                )
                ON CONFLICT ("Id") DO NOTHING;
                """);

            migrationBuilder.Sql("""
                SELECT setval(
                    pg_get_serial_sequence('"Products"', 'Id'),
                    COALESCE((SELECT MAX("Id") FROM "Products"), 1),
                    true
                );
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Geri dönüşte mevcut ürün verisini yanlışlıkla silmemek için boş bırakıldı.
        }
    }
}