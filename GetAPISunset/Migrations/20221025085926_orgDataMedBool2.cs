using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetAPISunset.Migrations
{
    public partial class orgDataMedBool2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SummerOrWinter",
                table: "SunTime",
                newName: "SummerWinter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SummerWinter",
                table: "SunTime",
                newName: "SummerOrWinter");
        }
    }
}
