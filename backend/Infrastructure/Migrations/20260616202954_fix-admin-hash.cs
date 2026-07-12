using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixadminhash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash", "PhoneNumber" },
                values: new object[] { "admin@gmail.com", "rJaJ4ickJwheNbnT4+i+2IyzQ0gotDuG/AWWytTG4nA=", "5550000001" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[,]
                {
                    { 2, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "hikmet@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000002", 2, "Hikmet Kütük" },
                    { 3, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "okan@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000003", 2, "Okan Demirkan" },
                    { 4, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "onur@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000004", 2, "Onur Demir" },
                    { 5, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "mustafa@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000005", 2, "Mustafa Ardınç" },
                    { 6, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "kurtulus@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000006", 2, "Kurtuluş Tekeci" },
                    { 7, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "samet@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000007", 2, "Samet Can Bayraktar" },
                    { 8, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "ebrar@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000008", 2, "Ebar Karabacak" },
                    { 9, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "tugra@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000009", 2, "Tuğra Mert Nehir" },
                    { 10, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "bayram@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000010", 2, "Bayram Furkan Korkmaz" },
                    { 11, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "mustafakarabacak@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000011", 2, "Mustafa Karabacak" },
                    { 12, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "osman@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000012", 2, "Osman Korkmaz" },
                    { 13, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "kodal@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000013", 2, "Muhammet Kodal" },
                    { 14, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "çetin@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000014", 2, "Abdullah Çetin" },
                    { 15, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "ismail@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000015", 2, "İsmail Sercen Öztürk" },
                    { 16, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "kemal@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000016", 2, "Kemal Ayhan" },
                    { 17, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "şükran@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000017", 2, "Şükran Kayabaşıoğlu" },
                    { 18, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "murat@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000018", 2, "Murat Salihoğlu" },
                    { 19, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "faik@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000019", 2, "Faik Emre Pusat" },
                    { 20, new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "ömer@gmail.com", "gxwjeSjmISvtqkRRpRSs4xdFYvZ2H2oVei/lCCs24vs=", "5550000020", 2, "Ömer Saroğlu" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash", "PhoneNumber" },
                values: new object[] { "demirkanokan10@gmail.com", "DoWDPXQrE1TFLgFhjyP3nu+ZKOj6XN9OrulSx0mzgTc=", "5555554586" });
        }
    }
}
