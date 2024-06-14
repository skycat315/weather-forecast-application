// This is a model class representing the UserPreferences table

namespace WeatherForecastApplication.Models
{
    public class UserPreference
    {
        public string UserID { get; set; }
        public int LocationID { get; set; }

        // Navigation property
        public Location Location { get; set; }

        // Composite primary key configuration
        public override int GetHashCode()
        {
            return HashCode.Combine(UserID, LocationID);
        }
    }

}
