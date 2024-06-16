// This class represents the ApplicationDbContext for the WeatherForecastApplication, 
// providing access to database tables for locations, weather conditions, and user preferences

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

    }
}
