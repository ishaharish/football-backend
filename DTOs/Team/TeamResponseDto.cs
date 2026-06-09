namespace football_backend.DTOs.Team
{
    public class TeamResponseDto
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public int VoteCount { get; set; }
    }
}
