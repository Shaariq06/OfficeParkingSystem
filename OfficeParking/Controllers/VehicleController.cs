using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeParkingSystem.Models.DTO;
using OfficeParkingSystem.Services;
using System.Security.Claims;

namespace OfficeParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IDvsaService _dvsaService;

        public VehicleController(IVehicleService vehicleService, IDvsaService dvsaService)
        {
            _vehicleService = vehicleService;
            _dvsaService = dvsaService;
        }

        // GET: api/vehicle
        [HttpGet]
        public async Task<IActionResult> GetUserVehicles()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var vehicles = await _vehicleService.GetVehiclesByUserIdAsync(userId);
            return Ok(vehicles);
        }

        // GET: api/vehicle/dvsa/{regNo}
        [HttpGet("dvsa/{regNo}")]
        public async Task<IActionResult> GetDvsaDetails(string regNo)
        {
            if (string.IsNullOrWhiteSpace(regNo))
            {
                return BadRequest("Registration number cannot be empty.");
            }

            var vehicleDetails = await _dvsaService.GetVehicleDetailsAsync(regNo);
            Console.WriteLine(vehicleDetails);

            if (vehicleDetails == null)
            {
                return NotFound(new { message = $"Vehicle with registration '{regNo}' not found in DVSA records." });
            }

            return Ok(vehicleDetails);
        }

        // POST: api/vehicle
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] AddVehicleRequestDTO addVehicleRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found.");
            }

            try
            {
                var newVehicle = await _vehicleService.AddVehicleAsync(userId, addVehicleRequestDTO);
                return CreatedAtAction(nameof(AddVehicle), new { id = newVehicle.VehicleId }, newVehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/vehicle/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var success = await _vehicleService.DeleteVehicleAsync(id, userId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/vehicle/search/{regNo}
        [HttpGet("search/{regNo}")]
        public async Task<IActionResult> SearchVehicle(string regNo)
        {
            var owner = await _vehicleService.GetVehicleOwnerAsync(regNo);

            if (owner == null)
            {
                return NotFound($"Vehicle with registration '{regNo}' not found.");
            }

            var ownerInfo = new
            {
                owner.FirstName,
                owner.LastName,
                owner.PhoneNo
            };

            return Ok(ownerInfo);
        }
    }
}
