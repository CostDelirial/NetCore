using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SuspensionesAPI.Migrations
{
    public partial class ducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cat_ducto",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(nullable: false),
                    estatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_ducto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cat_logistica",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_logistica", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cat_personalCC",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_personalCC", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    control = table.Column<int>(nullable: false),
                    nombre = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    tipo = table.Column<int>(nullable: false),
                    estatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cat_motivoSuspension",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(nullable: false),
                    logisticaid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_motivoSuspension", x => x.id);
                    table.ForeignKey(
                        name: "FK_cat_motivoSuspension_cat_logistica_logisticaid",
                        column: x => x.logisticaid,
                        principalTable: "cat_logistica",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suspensiones",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estatus = table.Column<string>(nullable: true),
                    fechaHora = table.Column<DateTime>(nullable: false),
                    observaciones = table.Column<string>(nullable: true),
                    km = table.Column<double>(nullable: false),
                    bph = table.Column<int>(nullable: false),
                    bls = table.Column<int>(nullable: false),
                    seregistro = table.Column<DateTime>(nullable: false),
                    ductoId = table.Column<int>(nullable: false),
                    motivoSuspensionId = table.Column<int>(nullable: false),
                    personalCCId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suspensiones", x => x.id);
                    table.ForeignKey(
                        name: "FK_suspensiones_cat_ducto_ductoId",
                        column: x => x.ductoId,
                        principalTable: "cat_ducto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suspensiones_cat_motivoSuspension_motivoSuspensionId",
                        column: x => x.motivoSuspensionId,
                        principalTable: "cat_motivoSuspension",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suspensiones_cat_personalCC_personalCCId",
                        column: x => x.personalCCId,
                        principalTable: "cat_personalCC",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cat_motivoSuspension_logisticaid",
                table: "cat_motivoSuspension",
                column: "logisticaid");

            migrationBuilder.CreateIndex(
                name: "IX_suspensiones_ductoId",
                table: "suspensiones",
                column: "ductoId");

            migrationBuilder.CreateIndex(
                name: "IX_suspensiones_motivoSuspensionId",
                table: "suspensiones",
                column: "motivoSuspensionId");

            migrationBuilder.CreateIndex(
                name: "IX_suspensiones_personalCCId",
                table: "suspensiones",
                column: "personalCCId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "suspensiones");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "cat_ducto");

            migrationBuilder.DropTable(
                name: "cat_motivoSuspension");

            migrationBuilder.DropTable(
                name: "cat_personalCC");

            migrationBuilder.DropTable(
                name: "cat_logistica");
        }
    }
}
