using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public interface IDvsaService
    {
        Task<DvsaVehicleDetailsDTO?> GetVehicleDetailsAsync(string registration);
    }
}
