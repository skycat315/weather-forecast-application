// This is a controller class that manages the home, privacy, and error views for the WeatherForecastApplication

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherForecastApplication.Models;

namespace WeatherForecastApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Get: Home
        public IActionResult Index()
        {
            return View();
        }

        // Get: Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
