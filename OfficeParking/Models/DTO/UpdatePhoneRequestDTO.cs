using System.ComponentModel.DataAnnotations;

namespace OfficeParkingSystem.Models.DTO
{
    public class UpdatePhoneRequestDTO
    {
        [Required]
        [Phone]
        public required string NewPhoneNo { get; set; }
    }
}