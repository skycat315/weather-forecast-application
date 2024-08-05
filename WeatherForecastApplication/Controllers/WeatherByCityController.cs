// This is a controller that handles displaying weather information for a city selected by the user

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherForecastApplication.Data;
using WeatherForecastApplication.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecastApplication.Controllers
{
    public class WeatherByCityController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public WeatherByCityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeatherByCity
        public async Task<IActionResult> Index()
        {
            // Retrieve all locations from the database for the dropdown
            var locations = await _context.Locations.ToListAsync();
            ViewData["Locations"] = locations;

            // Initialize ViewData with an empty list for weather conditions
            ViewData["WeatherConditions"] = new List<WeatherCondition>();

            return View();
        }

        // POST: WeatherByCity
        [HttpPost]
        public async Task<IActionResult> Index(int? locationId)
        {
            // Retrieve all locations from the database for the dropdown
            var locations = await _context.Locations.ToListAsync();
            ViewData["Locations"] = locations;

            // Get the name of the selected city, or default to "Select City"
            var selectedCity = locationId.HasValue
                ? (await _context.Locations.FindAsync(locationId.Value))?.CityName
                : "Select City";

            // Retrieve weather conditions for the selected city
            var weatherConditions = locationId.HasValue
                ? await _context.WeatherConditions
                    .Where(w => w.LocationID == locationId)
                    .Include(w => w.Location) // Include Location data
                    .ToListAsync()
                : new List<WeatherCondition>();

            // Store data in ViewData for use in the view
            ViewData["SelectedLocation"] = locationId;
            ViewData["SelectedCityName"] = selectedCity;
            ViewData["WeatherConditions"] = weatherConditions;

            return View();
        }
    }
}
