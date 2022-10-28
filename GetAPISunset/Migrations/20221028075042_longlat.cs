using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetAPISunset.Migrations
{
    public partial class longlat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "SunTime",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "SunTime",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "OriginalSunrise",
                table: "SunTime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalSunset",
                table: "SunTime",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SummerWinter",
                table: "SunTime",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "SunTime");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "SunTime");

            migrationBuilder.DropColumn(
                name: "OriginalSunrise",
                table: "SunTime");

            migrationBuilder.DropColumn(
                name: "OriginalSunset",
                table: "SunTime");

            migrationBuilder.DropColumn(
                name: "SummerWinter",
                table: "SunTime");
        }
    }
}
