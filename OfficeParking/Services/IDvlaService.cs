using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public interface IDvlaService
    {
        Task<DvlaVehicleResponseDTO?> GetVehicleDetailsAsync(string regNo);
    }
}
