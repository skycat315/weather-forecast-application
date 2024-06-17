// This is a model class representing the Locations table

using WeatherForecastApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecastApplication.Models
{
    public class Location
    {
        // LocationID (Primary key)
        [Key] 
        public int LocationID { get; set; }

        // City Name
        [Required(ErrorMessage = "City Name is required")]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

    }

}
