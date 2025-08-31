using System.ComponentModel.DataAnnotations;

namespace OfficeParkingSystem.Models.Domain
{
    public class ParkingSpot
    {
        public int Id { get; set; }
        public required string SpotNumber { get; set; }
        public bool IsOccupied { get; set; } = false;
        public string? OccupiedByUserId { get; set; }
        public User? OccupiedByUser { get; set; }
        public int? OccupiedByVehicleId { get; set; }
        public Vehicle? OccupiedByVehicle { get; set; }
    }
}