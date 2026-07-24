using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class ImproveDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SpecialRequests_BookingId",
                table: "SpecialRequests",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TravelDate",
                table: "Bookings",
                column: "TravelDate");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialRequests_Bookings_BookingId",
                table: "SpecialRequests",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialRequests_Bookings_BookingId",
                table: "SpecialRequests");

            migrationBuilder.DropIndex(
                name: "IX_SpecialRequests_BookingId",
                table: "SpecialRequests");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TravelDate",
                table: "Bookings");
        }
    }
}
