using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeParking.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleToParkingSpot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OccupiedByVehicleId",
                table: "ParkingSpots",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OccupiedByVehicleVehicleId",
                table: "ParkingSpots",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_OccupiedByVehicleVehicleId",
                table: "ParkingSpots",
                column: "OccupiedByVehicleVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_Vehicles_OccupiedByVehicleVehicleId",
                table: "ParkingSpots",
                column: "OccupiedByVehicleVehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_Vehicles_OccupiedByVehicleVehicleId",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_OccupiedByVehicleVehicleId",
                table: "ParkingSpots");

            migrationBuilder.DropColumn(
                name: "OccupiedByVehicleId",
                table: "ParkingSpots");

            migrationBuilder.DropColumn(
                name: "OccupiedByVehicleVehicleId",
                table: "ParkingSpots");
        }
    }
}
