using System.ComponentModel.DataAnnotations;

namespace OfficeParkingSystem.Models.DTO
{
    public class AdminResetPasswordDTO
    {
        public required string NewPassword { get; set; }
    }
}
