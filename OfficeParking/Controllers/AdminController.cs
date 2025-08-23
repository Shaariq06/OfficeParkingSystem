using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeParkingSystem.Services;
using OfficeParkingSystem.Models.DTO;
using System.Security.Claims;

namespace OfficeParkingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "System Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: api/admin/users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var currentAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentAdminId))
            {
                return Unauthorized();
            }

            var users = await _adminService.GetAllUsersAsync(currentAdminId);
            return Ok(users);
        }

        // DELETE: api/admin/users/{id}
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var success = await _adminService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound("User not found.");
            }
            return NoContent();
        }

        // POST: api/admin/users/{id}/reset-password
        [HttpPost("users/{id}/reset-password")]
        public async Task<IActionResult> ResetPassword(string id, [FromBody] AdminResetPasswordDTO dto)
        {
            var success = await _adminService.ResetUserPasswordAsync(id, dto.NewPassword);

            if (!success)
            {
                return BadRequest("Password reset failed. The user may not exist or the new password is not valid.");
            }

            return Ok("Password has been reset successfully.");
        }
    }
}