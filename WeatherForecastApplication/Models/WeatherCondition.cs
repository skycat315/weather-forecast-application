// This is a model class representing the WeatherConditions table

using WeatherForecastApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecastApplication.Models
{
    public class WeatherCondition
    {
        // ConditionID (Primary key)
        [Key]
        public int ConditionID { get; set; }


        // Location ID (Foreign key)
        [Required(ErrorMessage = "Location is required")]
        public int LocationID { get; set; }

        // DateTime
        [Required(ErrorMessage = "Date and time is required")]
        public DateTime DateTime { get; set; }

        // Tempratur

        [Required(ErrorMessage = "Temperature is required")]
        public float Temperature { get; set; }

        // Humidity
        [Required(ErrorMessage = "Humidity is required")]
        public float Humidity { get; set; }

        // WindSpeed

        [Required(ErrorMessage = "Wind speed is required")]
        public float WindSpeed { get; set; }

        // Parent reference
        public Location? Location { get; set; }
    }

}
