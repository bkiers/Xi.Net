using Microsoft.EntityFrameworkCore.Migrations;

namespace Xi.Database.Migrations
{
    public partial class GameEloRatingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EloRatingChange",
                table: "Games",
                newName: "EloRatingChangeRed");

            migrationBuilder.AddColumn<int>(
                name: "EloRatingChangeBlack",
                table: "Games",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EloRatingChangeBlack",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "EloRatingChangeRed",
                table: "Games",
                newName: "EloRatingChange");
        }
    }
}
