using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace OfficeParkingSystem.Models.Domain
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [StringLength(10)]
        public required string RegNo { get; set; }
        [StringLength(50)]
        public required string Make { get; set; }
        [StringLength(50)]
        public required string Model { get; set; }
        [StringLength(50)]
        public required string Colour { get; set; }
        public required int Year { get; set; }
        public required string UserId { get; set; }
        public required User User { get; set; }
    }
}