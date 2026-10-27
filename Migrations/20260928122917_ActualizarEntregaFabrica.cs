    using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GranjaLechera.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarEntregaFabrica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadTachos",
                table: "EntregasFabrica",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EstadoEnvio",
                table: "EntregasFabrica",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstadoSolicitud",
                table: "EntregasFabrica",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TachosAceptados",
                table: "EntregasFabrica",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TachosRechazados",
                table: "EntregasFabrica",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TachosVaciosDevueltos",
                table: "EntregasFabrica",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "VolumenSolicitado",
                table: "EntregasFabrica",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadTachos",
                table: "EntregasFabrica");

            migrationBuilder.DropColumn(
                name: "EstadoEnvio",
                table: "EntregasFabrica");

            migrationBuilder.DropColumn(
                name: "EstadoSolicitud",
                table: "EntregasFabrica");

            migrationBuilder.DropColumn(
                name: "TachosAceptados",
                table: "EntregasFabrica");

            migrationBuilder.DropColumn(
                name: "TachosRechazados",
                table: "EntregasFabrica");

            migrationBuilder.DropColumn(
                name: "TachosVaciosDevueltos",
                table: "EntregasFabrica");

            migrationBuilder.DropColumn(
                name: "VolumenSolicitado",
                table: "EntregasFabrica");
        }
    }
}
