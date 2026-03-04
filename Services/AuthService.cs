using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateMyMusicAuth.Data;
using RateMyMusicAuth.DTOs;
using RateMyMusicAuth.Interfaces;
using RateMyMusicAuth.Models;

namespace RateMyMusicAuth.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(AppDbContext context, IJwtProvider jwtProvider)
        {
            _context = context;
            _jwtProvider = jwtProvider;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // Verificar si el email ya existe
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("El email ya se encuentra registrado.");
            }

            // Encriptar la contraseña usando BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Crear el usuario
            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Crear el perfil asociado
            var profile = new Profile
            {
                UserId = user.Id,
                Username = request.Username,
                User = user
            };

            user.Profile = profile;

            // Guardar en la base de datos
            await _context.Users.AddAsync(user);
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            // Generar token JWT
            var token = _jwtProvider.GenerateToken(user, profile.Username);

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                Username = profile.Username,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            // Buscar el usuario por email e incluir su perfil
            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Credenciales inv&#225;lidas.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("El usuario se encuentra inactivo.");
            }

            var username = user.Profile?.Username ?? string.Empty;

            // Generar token JWT
            var token = _jwtProvider.GenerateToken(user, username);

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                Username = username,
                Role = user.Role
            };
        }

        public async Task<bool> CompleteProfileAsync(Guid userId, CompleteProfileRequestDto request)
        {
            var user = await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new InvalidOperationException("User not found.");

            // Update role
            user.Role = request.Role;
            
            // Update profile
            if (user.Profile != null)
            {
                user.Profile.Bio = request.Bio;
                user.Profile.AvatarUrl = request.AvatarUrl;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProfileResponseDto> GetProfileAsync(Guid userId)
        {
            var user = await _context.Users.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || user.Profile == null) throw new InvalidOperationException("User or Profile not found.");

            return new ProfileResponseDto
            {
                Username = user.Profile.Username,
                AvatarUrl = user.Profile.AvatarUrl,
                Bio = user.Profile.Bio,
                Role = user.Role
            };
        }
    }
}
