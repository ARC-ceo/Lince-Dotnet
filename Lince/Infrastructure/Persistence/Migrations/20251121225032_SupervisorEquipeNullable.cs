using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lince.Migrations
{
    /// <inheritdoc />
    public partial class SupervisorEquipeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LC_SUPERVISOR_LC_EQUIPE_EquipeId",
                table: "LC_SUPERVISOR");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "LC_SUPERVISOR",
                type: "NVARCHAR2(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipeId",
                table: "LC_SUPERVISOR",
                type: "RAW(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "RAW(16)");

            migrationBuilder.AddForeignKey(
                name: "FK_LC_SUPERVISOR_LC_EQUIPE_EquipeId",
                table: "LC_SUPERVISOR",
                column: "EquipeId",
                principalTable: "LC_EQUIPE",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LC_SUPERVISOR_LC_EQUIPE_EquipeId",
                table: "LC_SUPERVISOR");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "LC_SUPERVISOR",
                type: "NVARCHAR2(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EquipeId",
                table: "LC_SUPERVISOR",
                type: "RAW(16)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "RAW(16)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LC_SUPERVISOR_LC_EQUIPE_EquipeId",
                table: "LC_SUPERVISOR",
                column: "EquipeId",
                principalTable: "LC_EQUIPE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
