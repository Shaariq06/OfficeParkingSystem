namespace OfficeParkingSystem.Models.DTO
{
    public class ParkingSpotDTO
    {
        public int Id { get; set; }
        public required string SpotNumber { get; set; }
        public bool IsOccupied { get; set; }
        public string? OccupiedBy { get; set; }
        public string? VehicleInfo { get; set; }
    }
}