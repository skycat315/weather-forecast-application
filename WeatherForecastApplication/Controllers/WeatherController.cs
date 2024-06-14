// This is a controller class responsible for handling HTTP requests related to weather forecasts, including actions for searching locations and retrieving weather information

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherForecastApplication.Data;
using WeatherForecastApplication.Models;
using System.Threading.Tasks;

namespace WeatherForecastApplication.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeatherController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var weatherConditions = await _context.WeatherConditions
                .Include(w => w.Location)
                .ToListAsync();
            return View(weatherConditions);
        }
    }
}
