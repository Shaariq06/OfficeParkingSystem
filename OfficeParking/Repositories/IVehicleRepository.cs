using OfficeParkingSystem.Models.Domain;

namespace OfficeParkingSystem.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task DeleteAsync(int vehicleId);
        Task<Vehicle?> GetByIdAsync(int vehicleId);
        Task<Vehicle?> GetByRegNoWithUserAsync(string regNo);
        Task<List<Vehicle>> GetByUserIdAsync(string userId);
        Task<Vehicle?> GetByRegNoAsync(string regNo);
    }
}