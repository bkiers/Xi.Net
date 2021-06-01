using Microsoft.EntityFrameworkCore.Migrations;

namespace Xi.Database.Migrations
{
    public partial class GameAddGameResultType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameResultType",
                table: "Games",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameResultType",
                table: "Games");
        }
    }
}
