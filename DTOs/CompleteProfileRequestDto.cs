using System.ComponentModel.DataAnnotations;

namespace RateMyMusicAuth.DTOs
{
    public class CompleteProfileRequestDto
    {
        [Required]
        public string Role { get; set; } = "User";

        public string? Bio { get; set; }

        public string? AvatarUrl { get; set; }
    }
}
