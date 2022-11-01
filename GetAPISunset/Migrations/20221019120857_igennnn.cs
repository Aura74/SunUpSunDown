using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetAPISunset.Migrations
{
    public partial class igennnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "SunTime",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "SunTime");
        }
    }
}
