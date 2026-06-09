using System.ComponentModel.DataAnnotations;

namespace football_backend.DTOs.Poll
{
    public class VoteDto
    {
        [Required]
        public int TeamId { get; set; }
    }
}
