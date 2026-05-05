using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyMusicAuth.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Token { get; set; } = string.Empty;

        public string JwtId { get; set; } = string.Empty;

        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; }

        // Relación con User
        public Guid UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
