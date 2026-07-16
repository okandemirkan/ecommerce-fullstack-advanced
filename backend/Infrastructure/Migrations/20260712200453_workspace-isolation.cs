using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class workspaceisolation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Reviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "OrderItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "CartItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Addresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDemo = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkspaceId",
                table: "Users",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WorkspaceId",
                table: "Reviews",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WorkspaceId",
                table: "Products",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WorkspaceId",
                table: "Orders",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_WorkspaceId",
                table: "OrderItems",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_WorkspaceId_CategoryName",
                table: "Categories",
                columns: new[] { "WorkspaceId", "CategoryName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_WorkspaceId",
                table: "CartItems",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_WorkspaceId",
                table: "Addresses",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_ExpiresAt",
                table: "Workspaces",
                column: "ExpiresAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Workspaces_WorkspaceId",
                table: "Addresses",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Workspaces_WorkspaceId",
                table: "CartItems",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Workspaces_WorkspaceId",
                table: "Categories",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Workspaces_WorkspaceId",
                table: "OrderItems",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Workspaces_WorkspaceId",
                table: "Orders",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Workspaces_WorkspaceId",
                table: "Products",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Workspaces_WorkspaceId",
                table: "Reviews",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                table: "Users",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Workspaces_WorkspaceId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Workspaces_WorkspaceId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Workspaces_WorkspaceId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Workspaces_WorkspaceId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Workspaces_WorkspaceId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Workspaces_WorkspaceId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Workspaces_WorkspaceId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workspaces_WorkspaceId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Users_WorkspaceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_WorkspaceId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Products_WorkspaceId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WorkspaceId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_WorkspaceId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Categories_WorkspaceId_CategoryName",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_WorkspaceId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_WorkspaceId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);
        }
    }
}
