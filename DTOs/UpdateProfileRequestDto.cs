using System.ComponentModel.DataAnnotations;

namespace RateMyMusicAuth.DTOs
{
    public class UpdateProfileRequestDto
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El username debe tener entre 3 y 50 caracteres.")]
        public string? Username { get; set; }

        public string? Bio { get; set; }

        public string? AvatarUrl { get; set; }
    }
}
