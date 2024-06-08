using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBApp.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_BoatStandard_BoatStandardIdBoatStandard",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_BoatStandardIdBoatStandard",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "BoatStandardIdBoatStandard",
                table: "Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IdBoatStandard",
                table: "Reservation",
                column: "IdBoatStandard");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_BoatStandard_IdBoatStandard",
                table: "Reservation",
                column: "IdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_BoatStandard_IdBoatStandard",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_IdBoatStandard",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "BoatStandardIdBoatStandard",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BoatStandardIdBoatStandard",
                table: "Reservation",
                column: "BoatStandardIdBoatStandard");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_BoatStandard_BoatStandardIdBoatStandard",
                table: "Reservation",
                column: "BoatStandardIdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard");
        }
    }
}
