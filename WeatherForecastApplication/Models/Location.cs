namespace WeatherForecastApplication.Models
{
    public class Location
    {
        // Primary key
        public int LocationID { get; set; }

        // Other properties
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
