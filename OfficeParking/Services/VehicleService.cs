using OfficeParkingSystem.Models.Domain;
using OfficeParkingSystem.Models.DTO;
using OfficeParkingSystem.Repositories;

namespace OfficeParkingSystem.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDvlaService _dvlaService;

        public VehicleService(IVehicleRepository vehicleRepository, IDvlaService dvlaService)
        {
            _vehicleRepository = vehicleRepository;
            _dvlaService = dvlaService;
        }

        public async Task<Vehicle> AddVehicleAsync(string userId, AddVehicleRequestDTO dto)
        {
            var existingVehicle = await _vehicleRepository.GetByRegNoAsync(dto.RegNo);

            if (existingVehicle != null)
            {
                throw new Exception($"Vehicle with registration '{dto.RegNo}' is already registered.");
            }
            var vehicle = new Vehicle
            {
                RegNo = dto.RegNo.ToUpper().Replace(" ", string.Empty),
                Make = dto.Make,
                Model = "Golf",
                Year = dto.Year,
                Colour = dto.Colour,
                UserId = userId,
                User = null
            };

            return await _vehicleRepository.AddAsync(vehicle);
        }

        public async Task<bool> DeleteVehicleAsync(int vehicleId, string userId)
        {
            var vehicleToDelete = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicleToDelete != null && vehicleToDelete.UserId == userId)
            {
                await _vehicleRepository.DeleteAsync(vehicleId);
                return true;
            }
            return false;
        }

        public async Task<User?> GetVehicleOwnerAsync(string regNo)
        {
            var vehicle = await _vehicleRepository.GetByRegNoWithUserAsync(regNo);
            return vehicle?.User;
        }

        public async Task<List<Vehicle>> GetVehiclesByUserIdAsync(string userId)
        {
            return await _vehicleRepository.GetByUserIdAsync(userId);
        }
    }
}
