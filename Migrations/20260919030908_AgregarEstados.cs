using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GranjaLechera.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEstados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "TratamientosSanitarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "LotesAnalisisCalidad",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "EntregasFabrica",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "AnimalesVeterinarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "AnimalesDietas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "AnimalesCertificaciones",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "TratamientosSanitarios");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "LotesAnalisisCalidad");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "EntregasFabrica");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "AnimalesVeterinarios");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "AnimalesDietas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "AnimalesCertificaciones");
        }
    }
}
