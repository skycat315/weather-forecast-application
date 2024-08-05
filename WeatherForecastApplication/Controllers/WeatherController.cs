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
using Microsoft.AspNetCore.Authorization;

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
                .OrderBy(w => w.Date.Date) // Get only the date part and order by ascending
                .ToListAsync(); // Convert results to a list asynchronously

            // Display weather condition details
            return View(weatherConditions);
        }

        // GET: Weather/Details/5
        // Anyone can access this action to view details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the ID is null
            if (id == null)
            {
                return NotFound(); // Return 404
            }

            // Retrieve weather condition by ID with associated Location data
            var weatherCondition = await _context.WeatherConditions
                .Include(w => w.Location) // Include related Location data
                .FirstOrDefaultAsync(m => m.ConditionID == id); // Find by ConditionID

            // Check if the weather condition exists
            if (weatherCondition == null)
            {
                return NotFound(); // Return 404
            }

            // Display weather condition details
            return View(weatherCondition);
        }

        // GET: Weather/Create
        // Only logged-in users can add new data
        [Authorize]
        public IActionResult Create()
        {
            // Populate ViewData with a list of locations for dropdown
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "CityName");
            // Populate ViewData with weather condition options
            ViewData["Condition"] = new SelectList(Enum.GetValues(typeof(WeatherConditionType)));
            // Display the form for creating a new weather condition
            return View();
        }

        // POST: Weather/Create
        // Only logged-in users can add new data
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConditionID,LocationID,Date,Condition,Temperature,Humidity,WindSpeed")] WeatherCondition weatherCondition)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                _context.Add(weatherCondition); // Add the new weather condition to the context
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the index action or any other relevant action
            }

            // If ModelState is not valid, repopulate the dropdowns
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "CityName", weatherCondition.LocationID);
            ViewData["Condition"] = new SelectList(Enum.GetValues(typeof(WeatherConditionType)), weatherCondition.Condition);

            // If model state is not valid, re-render the form with validation errors
            return View(weatherCondition);
        }

        // GET: Weather/Edit/5
        // Only logged-in users can edit data
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the ID is null
            if (id == null)
            {
                return NotFound(); // Return 404
            }

            // Retrieve a weather condition from the database using its ID asynchronously
            var weatherCondition = await _context.WeatherConditions.FindAsync(id);
            // Check if weatherCondition is null (not found in the database)
            if (weatherCondition == null)
            {
                return NotFound(); // Return 404
            }

            // Populate ViewData with the current location for dropdown
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "CityName", weatherCondition.LocationID);
            ViewData["Condition"] = new SelectList(Enum.GetValues(typeof(WeatherConditionType)), weatherCondition.Condition);

            // Return the View with the located weather condition object
            return View(weatherCondition);
        }

        // POST: Weather/Edit/5
        // Only logged-in users can edit data
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConditionID,LocationID,Date,Condition,Temperature,Humidity,WindSpeed")] WeatherCondition weatherCondition)
        {
            // Check if the ID in the parameter does not match the ID in the weatherCondition object
            if (id != weatherCondition.ConditionID)
            {
                return NotFound(); // Return 404
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherCondition); // Update the weather condition in the context
                    await _context.SaveChangesAsync(); // Save changes to the database asynchronously
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the weather condition ID already exists
                    if (!WeatherConditionExists(weatherCondition.ConditionID))
                    {
                        return NotFound(); // Return 404
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirect to the index action after successful update
                return RedirectToAction(nameof(Index));
            }

            // If model state is not valid, re-render the form with validation errors
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "CityName", weatherCondition.LocationID);
            ViewData["Condition"] = new SelectList(Enum.GetValues(typeof(WeatherConditionType)), weatherCondition.Condition);

            return View(weatherCondition);
        }

        // GET: Weather/Delete/5
        // Only logged-in users can delete data
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the ID is null
            if (id == null)
            {
                return NotFound(); // Return 404
            }

            // Retrieve weather condition by ID with associated Location data
            var weatherCondition = await _context.WeatherConditions
                .Include(w => w.Location) // Include related Location data
                .FirstOrDefaultAsync(m => m.ConditionID == id); // Find by ConditionID

            // Check if the weather condition exists
            if (weatherCondition == null)
            {
                return NotFound(); // Return 404
            }

            // Display the confirmation view for deleting the weather condition
            return View(weatherCondition);
        }

        // POST: Weather/Delete/5
        // Only logged-in users can delete data
        [Authorize]
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
            // Check if any WeatherCondition entity has the specified ConditionID
            return _context.WeatherConditions.Any(e => e.ConditionID == id);
        }
    }
}
