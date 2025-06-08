using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HidroSafe.API.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTACOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataInstalacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTACOES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LEITURAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DistanciaMargemCm = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    EstacaoMonitoramentoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEITURAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LEITURAS_ESTACOES_EstacaoMonitoramentoId",
                        column: x => x.EstacaoMonitoramentoId,
                        principalTable: "ESTACOES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ESTACOES",
                columns: new[] { "Id", "DataInstalacao", "Localizacao", "Nome" },
                values: new object[] { 1, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rua do Córrego, 123", "Estação Rio do Meio" });

            migrationBuilder.InsertData(
                table: "LEITURAS",
                columns: new[] { "Id", "DataHora", "DistanciaMargemCm", "EstacaoMonitoramentoId" },
                values: new object[] { 1, new DateTime(2025, 6, 8, 14, 30, 0, 0, DateTimeKind.Unspecified), 18.5, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_LEITURAS_EstacaoMonitoramentoId",
                table: "LEITURAS",
                column: "EstacaoMonitoramentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LEITURAS");

            migrationBuilder.DropTable(
                name: "ESTACOES");
        }
    }
}
