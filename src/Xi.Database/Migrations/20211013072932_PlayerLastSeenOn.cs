using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xi.Database.Migrations
{
    public partial class PlayerLastSeenOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastSeenOn",
                table: "Players",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSeenOn",
                table: "Players");
        }
    }
}
