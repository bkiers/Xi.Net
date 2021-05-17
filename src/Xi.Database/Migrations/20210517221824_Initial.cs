using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Xi.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    EloRating = table.Column<int>(type: "integer", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Accepted = table.Column<bool>(type: "boolean", nullable: false),
                    SecondsPerMove = table.Column<int>(type: "integer", nullable: false),
                    EloRatingChange = table.Column<int>(type: "integer", nullable: true),
                    ClockRunsOutAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AcceptedDrawPlayerId = table.Column<int>(type: "integer", nullable: true),
                    InitiatedPlayerId = table.Column<int>(type: "integer", nullable: false),
                    InvitedPlayerId = table.Column<int>(type: "integer", nullable: false),
                    TurnPlayerId = table.Column<int>(type: "integer", nullable: false),
                    RedPlayerId = table.Column<int>(type: "integer", nullable: false),
                    BlackPlayerId = table.Column<int>(type: "integer", nullable: false),
                    WinnerPlayerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Players_AcceptedDrawPlayerId",
                        column: x => x.AcceptedDrawPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_BlackPlayerId",
                        column: x => x.BlackPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Players_InitiatedPlayerId",
                        column: x => x.InitiatedPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Players_InvitedPlayerId",
                        column: x => x.InvitedPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Players_RedPlayerId",
                        column: x => x.RedPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Players_TurnPlayerId",
                        column: x => x.TurnPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Players_WinnerPlayerId",
                        column: x => x.WinnerPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromRankIndex = table.Column<int>(type: "integer", nullable: false),
                    FromFileIndex = table.Column<int>(type: "integer", nullable: false),
                    ToRankIndex = table.Column<int>(type: "integer", nullable: false),
                    ToFileIndex = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_AcceptedDrawPlayerId_WinnerPlayerId_Accepted",
                table: "Games",
                columns: new[] { "AcceptedDrawPlayerId", "WinnerPlayerId", "Accepted" });

            migrationBuilder.CreateIndex(
                name: "IX_Games_BlackPlayerId",
                table: "Games",
                column: "BlackPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_InitiatedPlayerId",
                table: "Games",
                column: "InitiatedPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_InvitedPlayerId",
                table: "Games",
                column: "InvitedPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_RedPlayerId",
                table: "Games",
                column: "RedPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TurnPlayerId",
                table: "Games",
                column: "TurnPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerPlayerId",
                table: "Games",
                column: "WinnerPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_GameId",
                table: "Moves",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Email",
                table: "Players",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
