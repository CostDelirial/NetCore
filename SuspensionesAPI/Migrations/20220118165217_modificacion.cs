using Microsoft.EntityFrameworkCore.Migrations;

namespace SuspensionesAPI.Migrations
{
    public partial class modificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "duracion",
                table: "suspensiones",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duracion",
                table: "suspensiones");
        }
    }
}
