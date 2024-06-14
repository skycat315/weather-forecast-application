using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherForecastApplication.Models;

namespace WeatherForecastApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<WeatherCondition> WeatherConditions { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
                .HasKey(l => l.LocationID);

            modelBuilder.Entity<WeatherCondition>()
                .HasKey(w => w.ConditionID);

            modelBuilder.Entity<UserPreference>()
                .HasKey(up => new { up.UserID, up.LocationID });

            // Configure relationships (if any)
        }

    }
}
