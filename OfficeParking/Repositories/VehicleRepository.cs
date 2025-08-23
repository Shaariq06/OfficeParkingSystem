using Microsoft.EntityFrameworkCore;
using OfficeParkingSystem.Data;
using OfficeParkingSystem.Models.Domain;

namespace OfficeParkingSystem.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            vehicle.RegNo = vehicle.RegNo.Replace(" ", string.Empty);
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task DeleteAsync(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Vehicle?> GetByIdAsync(int vehicleId)
        {
            return await _context.Vehicles.FindAsync(vehicleId);
        }

        public async Task<Vehicle?> GetByRegNoWithUserAsync(string regNo)
        {
            return await _context.Vehicles
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.RegNo.ToLower().Replace(" ", string.Empty) == regNo.ToLower().Replace(" ", string.Empty));
        }

        public async Task<List<Vehicle>> GetByUserIdAsync(string userId)
        {
            return await _context.Vehicles
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        public async Task<Vehicle?> GetByRegNoAsync(string regNo)
        {
            var normalizedRegNo = regNo.ToLower().Replace(" ", string.Empty);
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.RegNo.ToLower().Replace(" ", string.Empty) == normalizedRegNo);
        }
    }
}
