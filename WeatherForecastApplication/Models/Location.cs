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

        // ProvinceName
        //[Required(ErrorMessage = "Province Name is required")]
        [Display(Name = "Province Name")]
        public string ProvinceName { get; set; }

        // Country Name
        [Required(ErrorMessage = "Country Name is required")]
        [Display(Name = "Cuntry Name")]
        public string CountryName { get; set; }


    }

}
