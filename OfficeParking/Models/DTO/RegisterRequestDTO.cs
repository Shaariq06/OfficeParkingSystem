using System.ComponentModel.DataAnnotations;

namespace OfficeParkingSystem.Models.DTO
{
    public class RegisterRequestDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [Phone]
        public required string PhoneNo { get; set; }
        public required string Password { get; set; }
    }
}