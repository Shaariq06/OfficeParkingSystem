using System.ComponentModel.DataAnnotations;

namespace OfficeParkingSystem.Models.DTO
{
    public class LoginRequestDTO
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}