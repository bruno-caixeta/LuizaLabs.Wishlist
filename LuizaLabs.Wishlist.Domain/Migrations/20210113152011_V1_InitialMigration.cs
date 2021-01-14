using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LuizaLabs.Wishlist.Domain.Migrations
{
    public partial class V1_InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Wishlist");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Wishlist",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                schema: "Wishlist",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Favorites_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Wishlist",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ClientId_ProductId",
                schema: "Wishlist",
                table: "Favorites",
                columns: new[] { "ClientId", "ProductId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites",
                schema: "Wishlist");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Wishlist");
        }
    }
}
