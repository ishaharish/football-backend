using football_backend.DTOs.Poll;
using football_backend.DTOs.Team;

namespace football_backend.Services
{
    public interface IPollService
    {
        Task<TeamResponseDto> AddTeamAsync(TeamCreateDto dto);
        Task ToggleResultVisibilityAsync();
        Task<bool> IsResultPublicAsync();
        Task<IEnumerable<TeamResponseDto>> GetTeamsAsync();
        Task VoteAsync(int userId, VoteDto dto);
    }
}
