using System;

namespace RateMyMusicAuth.Models
{
    public class Preference
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // Llave foránea para Profile
        public Guid ProfileId { get; set; }
        
        public string Genre { get; set; } = string.Empty;

        // Propiedad de navegación
        public Profile? Profile { get; set; }
    }
}
