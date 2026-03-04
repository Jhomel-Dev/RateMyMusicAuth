using System.Threading.Tasks;
using RateMyMusicAuth.DTOs;

namespace RateMyMusicAuth.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<bool> CompleteProfileAsync(Guid userId, CompleteProfileRequestDto request);
        Task<ProfileResponseDto> GetProfileAsync(Guid userId);
    }
}
