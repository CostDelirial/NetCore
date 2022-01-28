using Microsoft.EntityFrameworkCore.Migrations;

namespace SuspensionesAPI.Migrations
{
    public partial class corte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "corte",
                table: "suspensiones",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "corte",
                table: "suspensiones");
        }
    }
}
