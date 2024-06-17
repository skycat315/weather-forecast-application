// This is a controller for managing locations (cities) where weather forecasts are available

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastApplication.Data;
using WeatherForecastApplication.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherForecastApplication.Controllers
{
    public class LocationController : Controller
    {
        // Application database context
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            // Retrieve all weather conditions from the database
            var locations = await _context.Locations.ToListAsync();
            // Display layout details
            return View(locations);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View(); // Return the default view for creating a new resource
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationID,CityName")] Location location)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                _context.Add(location); // Add the new location to the context
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the index action or any other relevant action
            }

            // If ModelState is not valid, re-render the form with validation errors
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the ID is null
            if (id == null)
            {
                return NotFound(); // Return 404
            }

            // Retrieve a location from the database using its ID asynchronously
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound(); // Return 404
            }

            // Pass the location object to the view for editing
            return View(location);
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationID,CityName")] Location location)
        {
            // Check if the location with the specified ID exists
            if (id != location.LocationID)
            {
                return NotFound(); // Return 404
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location); // Update the location in the database context
                    await _context.SaveChangesAsync(); // Save changes asynchronously
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Check if the location with the given LocationID exists in the database
                    if (!LocationExists(location.LocationID))
                    {
                        return NotFound(); // Return 404
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirects to the Index action method to display the list of locations after successful creation
                return RedirectToAction(nameof(Index));
            }

            // Return the view with validation errors if ModelState is not valid
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the ID is null
            if (id == null)
            {
                return NotFound(); // Return 404
            }

            // Find the location in the database by ID
            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.LocationID == id);

            // Check if the location is not found
            if (location == null)
            {
                return NotFound(); // Error 404
            }

            // Return the location to the view for editing
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the location to delete by its ID asynchronously
            var location = await _context.Locations.FindAsync(id);

            _context.Locations.Remove(location); // Remove the location from the context
            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the Index action method to display the list of locations after successful deletion
        }

        private bool LocationExists(int id)
        {
            // Check if there exists any location in the database where LocationID matches the provided id
            return _context.Locations.Any(e => e.LocationID == id);
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the ID is null
            if (id == null)
            {
                return NotFound(); // Return 404
            }

            // Retrieve a location from the database using its ID asynchronously
            var location = await _context.Locations.FirstOrDefaultAsync(m => m.LocationID == id);

            // Check if location is null (not found in the database)
            if (location == null)
            {
                return NotFound(); // Return 404
            }

            // Check if location is null (not found in the database)
            return View(location);
        }
    }
}
