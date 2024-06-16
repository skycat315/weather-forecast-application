// This is a controller class responsible for handling HTTP requests related to weather forecasts, including actions for searching locations and retrieving weather information

using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherForecastApplication.Data;
using WeatherForecastApplication.Models;

namespace WeatherForecastApplication.Controllers
{
    public class WeatherController : Controller
    {
        // Application database context
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public WeatherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weather
        public async Task<IActionResult> Index()
        {
            // Retrieve all weather conditions from the database
            var weatherConditions = await _context.WeatherConditions
                .Include(w => w.Location) // Include related Location data
                .OrderBy(w => w.DateTime) // Order by DateTime ascending
                .ToListAsync(); // Convert results to a list asynchronously

            // Display weather condition details
            return View(weatherConditions);
        }

        // GET: Weather/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the ID is null
            if (id == null)
            {
                return NotFound(); // Return 404 if ID is null
            }

            // Retrieve weather condition by ID with associated Location data
            var weatherCondition = await _context.WeatherConditions
                .Include(w => w.Location) // Include related Location data
                .FirstOrDefaultAsync(m => m.ConditionID == id); // Find by ConditionID

            // Check if the weather condition exists
            if (weatherCondition == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Display weather condition details
            return View(weatherCondition);
        }

        // GET: Weather/Create
        public IActionResult Create()
        {
            // Populate ViewData with a list of locations for dropdown
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "CityName");
            // Display the form for creating a new weather condition
            return View();
        }

        // POST: Weather/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConditionID,LocationID,DateTime,Temperature,Humidity,WindSpeed")] WeatherCondition weatherCondition)
        {

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                _context.Add(weatherCondition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, repopulate the Location dropdown
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "CityName", weatherCondition.LocationID);
            return View(weatherCondition);
        }

        // GET: Weather/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            var weatherCondition = _context.WeatherConditions.Find(id);
            // Check if the ID is null
            if (weatherCondition == null)
            {
                return NotFound(); // Return 404 if ID is null
            }

            // Populate ViewData with the current location for dropdown
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name", weatherCondition.LocationID);
            // Display the form for editing the weather condition
            return View(weatherCondition);
        }

        // POST: Weather/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConditionID,LocationID,DateTime,Temperature,Humidity,WindSpeed")] WeatherCondition weatherCondition)
        {
            // Check if the ID matches the condition ID
            if (id != weatherCondition.ConditionID)
            {
                return NotFound(); // Return 404 if IDs do not match
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherCondition); // Update the weather condition in the context
                    await _context.SaveChangesAsync(); // Save changes asynchronously to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the weather condition still exists
                    if (!WeatherConditionExists(weatherCondition.ConditionID))
                    {
                        return NotFound(); // Return 404 if not found
                    }
                    else
                    {
                        throw; // Rethrow if another concurrency exception occurs
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }

            // If the model state is not valid, redisplay the form with the provided data
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "Name", weatherCondition.LocationID);
            return View(weatherCondition);
        }

        // GET: Weather/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the ID is null
            if (id == null)
            {
                return NotFound(); // Return 404 if ID is null
            }

            // Retrieve weather condition by ID with associated Location data
            var weatherCondition = await _context.WeatherConditions
                .Include(w => w.Location) // Include related Location data
                .FirstOrDefaultAsync(m => m.ConditionID == id); // Find by ConditionID

            // Check if the weather condition exists
            if (weatherCondition == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Display the confirmation view for deleting the weather condition
            return View(weatherCondition);
        }

        // POST: Weather/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Retrieve the weather condition by ID
            var weatherCondition = await _context.WeatherConditions.FindAsync(id);
            // Check if the weather condition exists and remove it
            if (weatherCondition != null)
            {
                _context.WeatherConditions.Remove(weatherCondition); // Remove the weather condition from the context
            }

            // Save changes asynchronously to the database
            await _context.SaveChangesAsync();
            // Redirect to the Index action
            return RedirectToAction(nameof(Index));
        }

        // Check if a weather condition exists by ID
        private bool WeatherConditionExists(int id)
        {
            return _context.WeatherConditions.Any(e => e.ConditionID == id);
        }
    }
}
