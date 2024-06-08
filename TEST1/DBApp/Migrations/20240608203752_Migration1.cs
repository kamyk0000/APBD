using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBApp.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoatStandardIdBoatStandard",
                table: "Sailboat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoatStandardIdBoatStandard",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sailboat_Reservation",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false),
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sailboat_Reservation", x => new { x.IdReservation, x.IdSailboat });
                    table.ForeignKey(
                        name: "FK_Sailboat_Reservation_Reservation_IdReservation",
                        column: x => x.IdReservation,
                        principalTable: "Reservation",
                        principalColumn: "IdReservation",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sailboat_Reservation_Sailboat_IdSailboat",
                        column: x => x.IdSailboat,
                        principalTable: "Sailboat",
                        principalColumn: "IdSailboat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sailboat_BoatStandardIdBoatStandard",
                table: "Sailboat",
                column: "BoatStandardIdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BoatStandardIdBoatStandard",
                table: "Reservation",
                column: "BoatStandardIdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_Sailboat_Reservation_IdSailboat",
                table: "Sailboat_Reservation",
                column: "IdSailboat");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_BoatStandard_BoatStandardIdBoatStandard",
                table: "Reservation",
                column: "BoatStandardIdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard");

            migrationBuilder.AddForeignKey(
                name: "FK_Sailboat_BoatStandard_BoatStandardIdBoatStandard",
                table: "Sailboat",
                column: "BoatStandardIdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_BoatStandard_BoatStandardIdBoatStandard",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Sailboat_BoatStandard_BoatStandardIdBoatStandard",
                table: "Sailboat");

            migrationBuilder.DropTable(
                name: "Sailboat_Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Sailboat_BoatStandardIdBoatStandard",
                table: "Sailboat");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_BoatStandardIdBoatStandard",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "BoatStandardIdBoatStandard",
                table: "Sailboat");

            migrationBuilder.DropColumn(
                name: "BoatStandardIdBoatStandard",
                table: "Reservation");
        }
    }
}
