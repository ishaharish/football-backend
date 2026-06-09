using football_backend.DTOs.Team;
using football_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace football_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IPollService _pollService;

        public AdminController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet("teams")]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _pollService.GetAdminTeamsAsync();
            return Ok(teams);
        }

        [HttpPost("teams")]
        public async Task<IActionResult> AddTeam([FromBody] TeamCreateDto dto)
        {
            try
            {
                var team = await _pollService.AddTeamAsync(dto);
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("teams/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _pollService.DeleteTeamAsync(id);
            return Ok(new { message = "Team deleted successfully." });
        }

        [HttpPost("toggle-results")]
        public async Task<IActionResult> ToggleResults()
        {
            await _pollService.ToggleResultVisibilityAsync();
            var isPublic = await _pollService.IsResultPublicAsync();
            return Ok(new { message = $"Results are now {(isPublic ? "public" : "hidden")}." });
        }
    }
}
