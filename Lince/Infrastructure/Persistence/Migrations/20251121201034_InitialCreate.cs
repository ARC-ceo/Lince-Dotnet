using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lince.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LC_EQUIPE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_EQUIPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LC_SETOR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_SETOR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LC_OPERADOR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Funcao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    EquipeId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_OPERADOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LC_OPERADOR_LC_EQUIPE_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "LC_EQUIPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LC_SUPERVISOR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    EquipeId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_SUPERVISOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LC_SUPERVISOR_LC_EQUIPE_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "LC_EQUIPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LC_CAMERA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    SetorId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_CAMERA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LC_CAMERA_LC_SETOR_SetorId",
                        column: x => x.SetorId,
                        principalTable: "LC_SETOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LC_ALERTA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Motivo = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    NivelAlerta = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    OperadorId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    CameraId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LC_ALERTA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LC_ALERTA_LC_CAMERA_CameraId",
                        column: x => x.CameraId,
                        principalTable: "LC_CAMERA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LC_ALERTA_LC_OPERADOR_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "LC_OPERADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LC_ALERTA_CameraId",
                table: "LC_ALERTA",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_LC_ALERTA_OperadorId",
                table: "LC_ALERTA",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_LC_CAMERA_SetorId",
                table: "LC_CAMERA",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_LC_OPERADOR_EquipeId",
                table: "LC_OPERADOR",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_LC_SUPERVISOR_Email",
                table: "LC_SUPERVISOR",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LC_SUPERVISOR_EquipeId",
                table: "LC_SUPERVISOR",
                column: "EquipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LC_ALERTA");

            migrationBuilder.DropTable(
                name: "LC_SUPERVISOR");

            migrationBuilder.DropTable(
                name: "LC_CAMERA");

            migrationBuilder.DropTable(
                name: "LC_OPERADOR");

            migrationBuilder.DropTable(
                name: "LC_SETOR");

            migrationBuilder.DropTable(
                name: "LC_EQUIPE");
        }
    }
}
