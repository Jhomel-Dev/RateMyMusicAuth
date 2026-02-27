using Microsoft.EntityFrameworkCore;
using RateMyMusicAuth.Models;

namespace RateMyMusicAuth.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Preference> Preferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación: Un User tiene un único Profile (1 a 1 en cascada)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación: Un Profile tiene muchas Preference (1 a N en cascada)
            modelBuilder.Entity<Profile>()
                .HasMany(p => p.Preferences)
                .WithOne(pref => pref.Profile)
                .HasForeignKey(pref => pref.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
