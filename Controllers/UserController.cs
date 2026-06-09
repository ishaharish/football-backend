using football_backend.DTOs.Poll;
using football_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace football_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Admin")]
    public class UserController : ControllerBase
    {
        private readonly IPollService _pollService;

        public UserController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet("teams")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _pollService.GetTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("status")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStatus()
        {
            var isPublic = await _pollService.IsResultPublicAsync();
            return Ok(new { isPublic });
        }

        [HttpPost("vote")]
        public async Task<IActionResult> CastVote([FromBody] VoteDto dto)
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    return Unauthorized(new { message = "Invalid token." });
                }

                await _pollService.VoteAsync(userId, dto);
                return Ok(new { message = "Vote cast successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
