using OfficeParkingSystem.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OfficeParkingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
    }
}