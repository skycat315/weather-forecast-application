// This is a model class representing the Locations table

namespace WeatherForecastApplication.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string CityName { get; set; }

        // Navigation properties
        public ICollection<WeatherCondition> WeatherConditions { get; set; }
        public ICollection<UserPreference> UserPreferences { get; set; }
    }

}
