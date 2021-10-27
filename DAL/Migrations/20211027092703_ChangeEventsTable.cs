using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangeEventsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FlaggedAsEnded",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Uri",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "Events",
                newName: "EventApiId");

            migrationBuilder.RenameColumn(
                name: "MetroAreId",
                table: "Events",
                newName: "CityApiId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Events",
                newName: "ArtistApiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventApiId",
                table: "Events",
                newName: "VenueId");

            migrationBuilder.RenameColumn(
                name: "CityApiId",
                table: "Events",
                newName: "MetroAreId");

            migrationBuilder.RenameColumn(
                name: "ArtistApiId",
                table: "Events",
                newName: "ArtistId");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FlaggedAsEnded",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Popularity",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uri",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
