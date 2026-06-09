using football_backend.DTOs.Poll;
using football_backend.DTOs.Team;

namespace football_backend.Services
{
    public interface IPollService
    {
        Task<TeamResponseDto> AddTeamAsync(TeamCreateDto dto);
        Task DeleteTeamAsync(int teamId);
        Task ToggleResultVisibilityAsync();
        Task<bool> IsResultPublicAsync();
        Task<IEnumerable<TeamResponseDto>> GetTeamsAsync();
        Task<IEnumerable<TeamResponseDto>> GetAdminTeamsAsync();
        Task VoteAsync(int userId, VoteDto dto);
    }
}
