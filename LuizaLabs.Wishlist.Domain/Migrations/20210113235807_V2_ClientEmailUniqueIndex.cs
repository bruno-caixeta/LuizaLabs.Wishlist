using Microsoft.EntityFrameworkCore.Migrations;

namespace LuizaLabs.Wishlist.Domain.Migrations
{
    public partial class V2_ClientEmailUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                schema: "Wishlist",
                table: "Clients",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                schema: "Wishlist",
                table: "Clients");
        }
    }
}
