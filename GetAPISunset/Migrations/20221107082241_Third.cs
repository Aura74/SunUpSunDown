using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetAPISunset.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DagenDetGaller",
                table: "SunTime");

            migrationBuilder.RenameColumn(
                name: "sunset",
                table: "SunTime",
                newName: "Sunset");

            migrationBuilder.RenameColumn(
                name: "sunrise",
                table: "SunTime",
                newName: "Sunrise");

            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "SunTime",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "SunTime");

            migrationBuilder.RenameColumn(
                name: "Sunset",
                table: "SunTime",
                newName: "sunset");

            migrationBuilder.RenameColumn(
                name: "Sunrise",
                table: "SunTime",
                newName: "sunrise");

            migrationBuilder.AddColumn<string>(
                name: "DagenDetGaller",
                table: "SunTime",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
