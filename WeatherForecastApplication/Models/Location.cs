// This is a model class representing the Locations table

using WeatherForecastApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecastApplication.Models
{
    public class Location
    {
        // Primary key
        [Key] 
        public int LocationID { get; set; }

        [Required]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

    }

}
