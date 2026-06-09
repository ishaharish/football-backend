using System.ComponentModel.DataAnnotations;

namespace football_backend.DTOs.Team
{
    public class TeamCreateDto
    {
        [Required]
        public string TeamName { get; set; } = string.Empty;

        [Required, Url]
        public string LogoUrl { get; set; } = string.Empty;
    }
}
