// This is a model class representing the WeatherConditions table

using WeatherForecastApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecastApplication.Models
{
    public class WeatherCondition
    {
        // Primary key
        [Key]
        public int ConditionID { get; set; }
        public int LocationID { get; set; }
        public DateTime DateTime { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }

        // Child reference (Navigation property to Location)
        public Location Location { get; set; }
    }

}
