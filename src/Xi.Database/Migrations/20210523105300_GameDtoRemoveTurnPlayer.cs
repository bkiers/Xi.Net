using Microsoft.EntityFrameworkCore.Migrations;

namespace Xi.Database.Migrations
{
    public partial class GameDtoRemoveTurnPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_TurnPlayerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_TurnPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TurnPlayerId",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurnPlayerId",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_TurnPlayerId",
                table: "Games",
                column: "TurnPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_TurnPlayerId",
                table: "Games",
                column: "TurnPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
