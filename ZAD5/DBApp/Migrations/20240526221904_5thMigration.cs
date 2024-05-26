using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBApp.Migrations
{
    /// <inheritdoc />
    public partial class _5thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Prescriptions_IdPrescription",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Prescriptions_IdPrescription",
                table: "Patients");

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

            migrationBuilder.AddColumn<int>(
                name: "IdDoctor",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPatient",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PrescriptionMedicaments",
                columns: table => new
                {
                    IdPrescription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dose = table.Column<int>(type: "int", nullable: false),
                    IdMedicament = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedicaments", x => x.IdPrescription);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_IdDoctor",
                table: "Prescriptions",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_IdPatient",
                table: "Prescriptions",
                column: "IdPatient");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_IdDoctor",
                table: "Prescriptions",
                column: "IdDoctor",
                principalTable: "Doctors",
                principalColumn: "IdDoctor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_IdPatient",
                table: "Prescriptions",
                column: "IdPatient",
                principalTable: "Patients",
                principalColumn: "IdPatient",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_IdDoctor",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_IdPatient",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "PrescriptionMedicaments");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_IdDoctor",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_IdPatient",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "IdDoctor",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "IdPatient",
                table: "Prescriptions");

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
    }
}
