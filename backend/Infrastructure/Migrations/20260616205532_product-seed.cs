using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class productseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Price", "ProductName", "Stock" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "https://images.unsplash.com/photo-1695048133142-1a20484d2569?w=600", 54999.99m, "Apple iPhone 15 Pro", 50 },
                    { 2, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.", "https://images.unsplash.com/photo-1706439136251-58636289fc12?w=600", 49999.99m, "Samsung Galaxy S24 Ultra", 40 },
                    { 3, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum.", "https://images.unsplash.com/photo-1618366712010-f4ae9c647dcb?w=600", 8999.99m, "Sony WH-1000XM5 Kulaklık", 75 },
                    { 4, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia.", "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=600", 74999.99m, "Apple MacBook Pro 14\"", 20 },
                    { 5, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit.", "https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=600", 2499.99m, "Logitech MX Master 3 Mouse", 120 },
                    { 6, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut labore et dolore magnam aliquam quaerat voluptatem incididunt.", "https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=600", 34999.99m, "Samsung 4K OLED TV 55\"", 15 },
                    { 7, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet.", "https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=600", 42999.99m, "iPad Pro 12.9\" M4", 30 },
                    { 8, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse.", "https://images.unsplash.com/photo-1541140532154-b024d705b90a?w=600", 1899.99m, "Mechanical Gaming Klavye", 90 },
                    { 9, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. At vero eos et accusamus et iusto odio dignissimos ducimus blanditiis.", "https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=600", 13999.99m, "GoPro Hero 12 Black", 35 },
                    { 10, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus.", "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=600", 18499.99m, "Dell UltraSharp 27\" Monitör", 25 },
                    { 11, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam libero tempore cum soluta nobis eligendi optio cumque nihil impedit.", "https://images.unsplash.com/photo-1558317374-067fb5f30001?w=600", 19999.99m, "Dyson V15 Detect Süpürge", 18 },
                    { 12, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Itaque earum rerum hic tenetur a sapiente delectus ut aut reiciendis.", "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=600", 64999.99m, "Nikon Z6 III Fotoğraf Makinesi", 12 },
                    { 13, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quis nostrum exercitationem ullam corporis suscipit laboriosam nisi ut.", "https://images.unsplash.com/photo-1546868871-7041f2a55e12?w=600", 14999.99m, "Apple Watch Series 9", 60 },
                    { 14, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium.", "https://images.unsplash.com/photo-1585664811087-47f65abbad64?w=600", 4299.99m, "Philips Airfryer XXL", 45 },
                    { 15, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit fugit.", "https://images.unsplash.com/photo-1606813907291-d86efa9b94db?w=600", 24999.99m, "PlayStation 5 Slim", 22 },
                    { 16, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut enim ad minima veniam quis nostrum exercitationem ullam corporis.", "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=600", 6999.99m, "Bose SoundLink Max Hoparlör", 55 },
                    { 17, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam.", "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=600", 38999.99m, "LG Gram 16 Laptop", 28 },
                    { 18, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Et harum quidem rerum facilis est et expedita distinctio libero tempore.", "https://images.unsplash.com/photo-1589894404892-7310b92ea7a2?w=600", 9499.99m, "Xiaomi Robot Süpürge S20+", 33 },
                    { 19, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Temporibus autem quibusdam et aut officiis debitis rerum necessitatibus saepe.", "https://images.unsplash.com/photo-1510127034890-ba27508e9f1c?w=600", 29999.99m, "Canon EOS R50 Aynasız", 17 },
                    { 20, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Itaque earum rerum hic tenetur a sapiente delectus ut aut reiciendis voluptatibus.", "https://images.unsplash.com/photo-1591799264318-7e6ef8ddb7ea?w=600", 3799.99m, "Corsair RM1000x PSU", 42 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
