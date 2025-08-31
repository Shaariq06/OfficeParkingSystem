using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeParking.Migrations
{
    /// <inheritdoc />
    public partial class AddParkingSpotsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingSpots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SpotNumber = table.Column<string>(type: "TEXT", nullable: false),
                    IsOccupied = table.Column<bool>(type: "INTEGER", nullable: false),
                    OccupiedByUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSpots_AspNetUsers_OccupiedByUserId",
                        column: x => x.OccupiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_OccupiedByUserId",
                table: "ParkingSpots",
                column: "OccupiedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpots");
        }
    }
}
