// This is a model class representing the UserPreferences table

using WeatherForecastApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecastApplication.Models
{
    public class UserPreference
    {
        // Primary key
        [Key]
        public int ConditionID { get; set; }

        public int LocationID { get; set; }

        // Child reference (Navigation property to Location)
        public Location Location { get; set; }
    }
}
