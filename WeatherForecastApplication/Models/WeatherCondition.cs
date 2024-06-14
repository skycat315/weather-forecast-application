// This is a model class representing the WeatherConditions table

namespace WeatherForecastApplication.Models
{
    public class WeatherCondition
    {
        public int ConditionID { get; set; }
        public int LocationID { get; set; }
        public DateTime DateTime { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }

        // Navigation property
        public Location Location { get; set; }
    }

}
