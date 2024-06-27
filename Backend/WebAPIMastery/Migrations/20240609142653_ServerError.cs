using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIMastery.Migrations
{
    public partial class ServerError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Details_PersonId",
                table: "Details");

            migrationBuilder.CreateIndex(
                name: "IX_Details_PersonId",
                table: "Details",
                column: "PersonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Details_PersonId",
                table: "Details");

            migrationBuilder.CreateIndex(
                name: "IX_Details_PersonId",
                table: "Details",
                column: "PersonId");
        }
    }
}
