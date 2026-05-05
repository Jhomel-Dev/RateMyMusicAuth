using System.ComponentModel.DataAnnotations;

namespace RateMyMusicAuth.DTOs
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
