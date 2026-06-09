using football_backend.Data;
using football_backend.DTOs.Poll;
using football_backend.DTOs.Team;
using football_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace football_backend.Services
{
    public class PollService : IPollService
    {
        private readonly AppDbContext _context;

        public PollService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TeamResponseDto> AddTeamAsync(TeamCreateDto dto)
        {
            if (await _context.Teams.AnyAsync(t => t.TeamName == dto.TeamName))
            {
                throw new Exception("Team already exists.");
            }

            var team = new Team
            {
                TeamName = dto.TeamName,
                LogoUrl = dto.LogoUrl,
                VoteCount = 0
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return new TeamResponseDto
            {
                Id = team.Id,
                TeamName = team.TeamName,
                LogoUrl = team.LogoUrl,
                VoteCount = team.VoteCount
            };
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ToggleResultVisibilityAsync()
        {
            var config = await _context.SystemConfigs.FirstOrDefaultAsync(c => c.Id == 1);
            if (config == null)
            {
                config = new SystemConfig { Id = 1, IsResultPublic = true };
                _context.SystemConfigs.Add(config);
            }
            else
            {
                config.IsResultPublic = !config.IsResultPublic;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsResultPublicAsync()
        {
            var config = await _context.SystemConfigs.FirstOrDefaultAsync(c => c.Id == 1);
            return config?.IsResultPublic ?? false;
        }

        public async Task<IEnumerable<TeamResponseDto>> GetTeamsAsync()
        {
            var isPublic = await IsResultPublicAsync();

            var teams = await _context.Teams.ToListAsync();

            return teams.Select(t => new TeamResponseDto
            {
                Id = t.Id,
                TeamName = t.TeamName,
                LogoUrl = t.LogoUrl,
                VoteCount = isPublic ? t.VoteCount : 0 // Hide count if not public
            });
        }

        public async Task<IEnumerable<TeamResponseDto>> GetAdminTeamsAsync()
        {
            var teams = await _context.Teams.ToListAsync();

            return teams.Select(t => new TeamResponseDto
            {
                Id = t.Id,
                TeamName = t.TeamName,
                LogoUrl = t.LogoUrl,
                VoteCount = t.VoteCount // Admins always see votes
            });
        }

        public async Task VoteAsync(int userId, VoteDto dto)
        {
            if (await _context.Polls.AnyAsync(p => p.UserId == userId))
            {
                throw new Exception("User has already voted.");
            }

            var team = await _context.Teams.FindAsync(dto.TeamId);
            if (team == null)
            {
                throw new Exception("Team not found.");
            }

            var poll = new Poll
            {
                UserId = userId,
                TeamId = dto.TeamId
            };

            team.VoteCount++;

            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();
        }
    }
}
