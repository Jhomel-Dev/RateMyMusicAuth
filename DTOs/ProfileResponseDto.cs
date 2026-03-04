using System;

namespace RateMyMusicAuth.DTOs
{
    public class ProfileResponseDto
    {
        public string Username { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
