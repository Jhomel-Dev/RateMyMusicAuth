using System;
using System.Collections.Generic;

namespace RateMyMusicAuth.Models
{
    public class Profile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Llave foránea para User
        public Guid UserId { get; set; }
        
        public string Username { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }

        // Propiedades de navegación
        public User? User { get; set; }
        public ICollection<Preference> Preferences { get; set; } = new List<Preference>();
    }
}
