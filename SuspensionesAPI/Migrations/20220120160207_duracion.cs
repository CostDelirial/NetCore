using Microsoft.EntityFrameworkCore.Migrations;

namespace SuspensionesAPI.Migrations
{
    public partial class duracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "duracion",
                table: "suspensiones",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "duracion",
                table: "suspensiones",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
