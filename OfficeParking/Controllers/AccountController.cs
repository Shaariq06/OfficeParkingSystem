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
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // PUT: api/account/phone
        [HttpPut("phone")]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdatePhoneRequestDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var success = await _userService.UpdatePhoneNumberAsync(userId, dto);

            if (!success)
            {
                return NotFound("User not found.");
            }

            return Ok("Phone number updated successfully.");
        }

        // GET: api/account/profile
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))

            {
                return Unauthorized();
            }
            var userProfile = await _userService.GetUserProfileAsync(userId);
            if (userProfile == null)
            {
                return NotFound("User not found.");
            }
            return Ok(userProfile);
        }

        // GET: api/account/role
        [HttpGet("role")]
        public async Task<IActionResult> GetUserRole()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var role = await _userService.GetUserRoleAsync(userId);
            if (role == null)
            {
                return NotFound("User not found.");
            }
            return Ok(new { role });
        }
    }
}