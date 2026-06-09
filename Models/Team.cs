namespace football_backend.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public int VoteCount { get; set; } = 0;
    }
}
