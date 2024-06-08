using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBApp.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_BoatStandard_IdBoatStandard",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Sailboat_BoatStandard_BoatStandardIdBoatStandard",
                table: "Sailboat");

            migrationBuilder.DropIndex(
                name: "IX_Sailboat_BoatStandardIdBoatStandard",
                table: "Sailboat");

            migrationBuilder.DropColumn(
                name: "BoatStandardIdBoatStandard",
                table: "Sailboat");

            migrationBuilder.CreateIndex(
                name: "IX_Sailboat_IdBoatStandard",
                table: "Sailboat",
                column: "IdBoatStandard");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_BoatStandard_IdBoatStandard",
                table: "Reservation",
                column: "IdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sailboat_BoatStandard_IdBoatStandard",
                table: "Sailboat",
                column: "IdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_BoatStandard_IdBoatStandard",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Sailboat_BoatStandard_IdBoatStandard",
                table: "Sailboat");

            migrationBuilder.DropIndex(
                name: "IX_Sailboat_IdBoatStandard",
                table: "Sailboat");

            migrationBuilder.AddColumn<int>(
                name: "BoatStandardIdBoatStandard",
                table: "Sailboat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sailboat_BoatStandardIdBoatStandard",
                table: "Sailboat",
                column: "BoatStandardIdBoatStandard");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_BoatStandard_IdBoatStandard",
                table: "Reservation",
                column: "IdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sailboat_BoatStandard_BoatStandardIdBoatStandard",
                table: "Sailboat",
                column: "BoatStandardIdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard");
        }
    }
}
