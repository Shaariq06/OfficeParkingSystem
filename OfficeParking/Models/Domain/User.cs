using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OfficeParkingSystem.Models.Domain
{
    public class User : IdentityUser
    {
        [StringLength(100)]
        public required string FirstName { get; set; }
        [StringLength(100)]
        public required string LastName { get; set; }
        [Phone]
        public required string PhoneNo { get; set; }
        public ICollection<Vehicle>? Vehicles { get; set; }
    }
}
