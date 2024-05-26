using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBApp.Migrations
{
    /// <inheritdoc />
    public partial class _4thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPrescription",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPrescription",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    IdPrecription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.IdPrecription);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_IdPrescription",
                table: "Patients",
                column: "IdPrescription");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_IdPrescription",
                table: "Doctors",
                column: "IdPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Prescriptions_IdPrescription",
                table: "Doctors",
                column: "IdPrescription",
                principalTable: "Prescriptions",
                principalColumn: "IdPrecription",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Prescriptions_IdPrescription",
                table: "Patients",
                column: "IdPrescription",
                principalTable: "Prescriptions",
                principalColumn: "IdPrecription",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Prescriptions_IdPrescription",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Prescriptions_IdPrescription",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Patients_IdPrescription",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_IdPrescription",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IdPrescription",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IdPrescription",
                table: "Doctors");
        }
    }
}
