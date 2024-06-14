using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(WeatherCondition model)
        {
            if (ModelState.IsValid)
            {
                // Save the model data to the database or perform other actions
                // Example: Save to database using Entity Framework Core
                // using (var context = new ApplicationDbContext())
                // {
                //     context.WeatherConditions.Add(model);
                //     context.SaveChanges();
                // }

                // Redirect to Index action after successful creation
                return RedirectToAction("Index");
            }

            // If model state is not valid, return the view with validation errors
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var viewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }
    }
}
