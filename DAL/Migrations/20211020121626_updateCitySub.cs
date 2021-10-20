using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class updateCitySub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CitySubscriptions_UserId",
                table: "CitySubscriptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CitySubscriptions_Users_UserId",
                table: "CitySubscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitySubscriptions_Users_UserId",
                table: "CitySubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CitySubscriptions_UserId",
                table: "CitySubscriptions");
        }
    }
}
