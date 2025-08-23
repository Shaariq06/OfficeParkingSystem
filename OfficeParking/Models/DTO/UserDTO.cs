namespace OfficeParkingSystem.Models.DTO
{
    public class UserDTO
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNo { get; set; }
        public List<VehicleDTO>? Vehicles { get; set; }
    }
}