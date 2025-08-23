using OfficeParkingSystem.Models.Domain;
using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public interface IVehicleService
    {
        Task<Vehicle> AddVehicleAsync(string userId, AddVehicleRequestDTO dto);
        Task<bool> DeleteVehicleAsync(int vehicleId, string userId);
        Task<User?> GetVehicleOwnerAsync(string regNo);
        Task<List<Vehicle>> GetVehiclesByUserIdAsync(string userId);
    }
}