namespace OfficeParkingSystem.Models.DTO
{
    public class AddVehicleRequestDTO
    {
        public required string RegNo { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required int Year { get; set; }
        public required string Colour { get; set; }

    }
}
