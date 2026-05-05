using System;

namespace RateMyMusicAuth.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
