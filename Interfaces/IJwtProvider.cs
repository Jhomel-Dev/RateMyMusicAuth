using RateMyMusicAuth.Models;

namespace RateMyMusicAuth.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user, string username);
    }
}
