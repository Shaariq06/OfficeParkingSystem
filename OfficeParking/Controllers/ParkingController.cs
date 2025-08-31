using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeParkingSystem.Services;
using System.Security.Claims;
using OfficeParkingSystem.Models.DTO;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ParkingController : ControllerBase
{
    private readonly IParkingService _parkingService;

    public ParkingController(IParkingService parkingService)
    {
        _parkingService = parkingService;
    }

    [HttpGet("spots")]
    public async Task<IActionResult> GetParkingSpots()
    {
        var spots = await _parkingService.GetAllSpotsAsync();
        return Ok(spots);
    }

    [HttpPost("spots/{spotId}/toggle")]
    public async Task<IActionResult> ToggleParkingSpot(int spotId, [FromBody] OccupySpotRequestDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var result = await _parkingService.ToggleSpotAsync(spotId, userId, request.VehicleId);

        if (!result.Success)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(new { message = result.Message });
    }
}