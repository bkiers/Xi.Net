using Microsoft.EntityFrameworkCore.Migrations;

namespace Xi.Database.Migrations
{
    public partial class GameAddProposedDrawPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProposedDrawPlayerId",
                table: "Games",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ProposedDrawPlayerId",
                table: "Games",
                column: "ProposedDrawPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_ProposedDrawPlayerId",
                table: "Games",
                column: "ProposedDrawPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_ProposedDrawPlayerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ProposedDrawPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ProposedDrawPlayerId",
                table: "Games");
        }
    }
}
