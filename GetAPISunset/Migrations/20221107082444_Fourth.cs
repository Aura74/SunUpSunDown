using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetAPISunset.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Day",
                table: "SunTime",
                newName: "Datum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Datum",
                table: "SunTime",
                newName: "Day");
        }
    }
}
