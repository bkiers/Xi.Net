using Microsoft.EntityFrameworkCore.Migrations;

namespace Xi.Database.Migrations
{
    public partial class MovesAddCapturedPiece : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CapturedPiece",
                table: "Moves",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapturedPiece",
                table: "Moves");
        }
    }
}
