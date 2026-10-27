using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GranjaLechera.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificacionesSanitarias",
                columns: table => new
                {
                    IdCertificacion = table.Column<string>(type: "text", nullable: false),
                    NombreNorma = table.Column<string>(type: "text", nullable: false),
                    OrganismoEmisor = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificacionesSanitarias", x => x.IdCertificacion);
                });

            migrationBuilder.CreateTable(
                name: "Granjas",
                columns: table => new
                {
                    IdGranja = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Ubicacion = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Granjas", x => x.IdGranja);
                });

            migrationBuilder.CreateTable(
                name: "TiposAnalisis",
                columns: table => new
                {
                    IdTipoAnalisis = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAnalisis", x => x.IdTipoAnalisis);
                });

            migrationBuilder.CreateTable(
                name: "Animales",
                columns: table => new
                {
                    IdAnimal = table.Column<string>(type: "text", nullable: false),
                    IdGranja = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoSalud = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animales", x => x.IdAnimal);
                    table.ForeignKey(
                        name: "FK_Animales_Granjas_IdGranja",
                        column: x => x.IdGranja,
                        principalTable: "Granjas",
                        principalColumn: "IdGranja",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LotesLeche",
                columns: table => new
                {
                    IdLote = table.Column<string>(type: "text", nullable: false),
                    IdGranja = table.Column<string>(type: "text", nullable: false),
                    FechaOrdenio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Turno = table.Column<string>(type: "text", nullable: false),
                    VolumenLitros = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Temperatura = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
                    Acidez = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotesLeche", x => x.IdLote);
                    table.ForeignKey(
                        name: "FK_LotesLeche_Granjas_IdGranja",
                        column: x => x.IdGranja,
                        principalTable: "Granjas",
                        principalColumn: "IdGranja",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimalesCertificaciones",
                columns: table => new
                {
                    IdAnimal = table.Column<string>(type: "text", nullable: false),
                    IdCertificacion = table.Column<string>(type: "text", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalesCertificaciones", x => new { x.IdAnimal, x.IdCertificacion });
                    table.ForeignKey(
                        name: "FK_AnimalesCertificaciones_Animales_IdAnimal",
                        column: x => x.IdAnimal,
                        principalTable: "Animales",
                        principalColumn: "IdAnimal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalesCertificaciones_CertificacionesSanitarias_IdCertifi~",
                        column: x => x.IdCertificacion,
                        principalTable: "CertificacionesSanitarias",
                        principalColumn: "IdCertificacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimalesDietas",
                columns: table => new
                {
                    IdAnimal = table.Column<string>(type: "text", nullable: false),
                    IdInsumo = table.Column<string>(type: "text", nullable: false),
                    ProporcionDiaria = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalesDietas", x => new { x.IdAnimal, x.IdInsumo });
                    table.ForeignKey(
                        name: "FK_AnimalesDietas_Animales_IdAnimal",
                        column: x => x.IdAnimal,
                        principalTable: "Animales",
                        principalColumn: "IdAnimal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimalesVeterinarios",
                columns: table => new
                {
                    IdAnimal = table.Column<string>(type: "text", nullable: false),
                    IdVeterinario = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalesVeterinarios", x => new { x.IdAnimal, x.IdVeterinario });
                    table.ForeignKey(
                        name: "FK_AnimalesVeterinarios_Animales_IdAnimal",
                        column: x => x.IdAnimal,
                        principalTable: "Animales",
                        principalColumn: "IdAnimal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TratamientosSanitarios",
                columns: table => new
                {
                    IdTratamiento = table.Column<string>(type: "text", nullable: false),
                    IdAnimal = table.Column<string>(type: "text", nullable: false),
                    TipoMedicamento = table.Column<string>(type: "text", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodoRetiroLeche = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamientosSanitarios", x => x.IdTratamiento);
                    table.ForeignKey(
                        name: "FK_TratamientosSanitarios_Animales_IdAnimal",
                        column: x => x.IdAnimal,
                        principalTable: "Animales",
                        principalColumn: "IdAnimal",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntregasFabrica",
                columns: table => new
                {
                    IdEntrega = table.Column<string>(type: "text", nullable: false),
                    IdLote = table.Column<string>(type: "text", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VolumenEntregado = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ResponsableRecepcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntregasFabrica", x => x.IdEntrega);
                    table.ForeignKey(
                        name: "FK_EntregasFabrica_LotesLeche_IdLote",
                        column: x => x.IdLote,
                        principalTable: "LotesLeche",
                        principalColumn: "IdLote",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LotesAnalisisCalidad",
                columns: table => new
                {
                    IdLote = table.Column<string>(type: "text", nullable: false),
                    IdTipoAnalisis = table.Column<string>(type: "text", nullable: false),
                    Resultado = table.Column<string>(type: "text", nullable: false),
                    FechaAnalisis = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotesAnalisisCalidad", x => new { x.IdLote, x.IdTipoAnalisis });
                    table.ForeignKey(
                        name: "FK_LotesAnalisisCalidad_LotesLeche_IdLote",
                        column: x => x.IdLote,
                        principalTable: "LotesLeche",
                        principalColumn: "IdLote",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LotesAnalisisCalidad_TiposAnalisis_IdTipoAnalisis",
                        column: x => x.IdTipoAnalisis,
                        principalTable: "TiposAnalisis",
                        principalColumn: "IdTipoAnalisis",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animales_IdGranja",
                table: "Animales",
                column: "IdGranja");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalesCertificaciones_IdCertificacion",
                table: "AnimalesCertificaciones",
                column: "IdCertificacion");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasFabrica_IdLote",
                table: "EntregasFabrica",
                column: "IdLote");

            migrationBuilder.CreateIndex(
                name: "IX_LotesAnalisisCalidad_IdTipoAnalisis",
                table: "LotesAnalisisCalidad",
                column: "IdTipoAnalisis");

            migrationBuilder.CreateIndex(
                name: "IX_LotesLeche_IdGranja",
                table: "LotesLeche",
                column: "IdGranja");

            migrationBuilder.CreateIndex(
                name: "IX_TratamientosSanitarios_IdAnimal",
                table: "TratamientosSanitarios",
                column: "IdAnimal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalesCertificaciones");

            migrationBuilder.DropTable(
                name: "AnimalesDietas");

            migrationBuilder.DropTable(
                name: "AnimalesVeterinarios");

            migrationBuilder.DropTable(
                name: "EntregasFabrica");

            migrationBuilder.DropTable(
                name: "LotesAnalisisCalidad");

            migrationBuilder.DropTable(
                name: "TratamientosSanitarios");

            migrationBuilder.DropTable(
                name: "CertificacionesSanitarias");

            migrationBuilder.DropTable(
                name: "LotesLeche");

            migrationBuilder.DropTable(
                name: "TiposAnalisis");

            migrationBuilder.DropTable(
                name: "Animales");

            migrationBuilder.DropTable(
                name: "Granjas");
        }
    }
}
