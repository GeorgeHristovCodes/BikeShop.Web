using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Controllers
{
    [Authorize]
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentalController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Rental/Create/5
        public async Task<IActionResult> Create(int id)
        {
            var bicycle = await _context.Bicycles.FindAsync(id);
            if (bicycle == null || bicycle.Type != BicycleType.ForRent)
                return NotFound();

            ViewBag.Bicycle = bicycle;

            var rental = new Rental
            {
                BicycleId = id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };

            return View(rental);
        }

        // POST: /Rental/Create
        [HttpPost]
        public async Task<IActionResult> Create(Rental rental)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            if (rental.StartDate >= rental.EndDate)
            {
                ModelState.AddModelError("", "Крайната дата трябва да е след началната.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Bicycle = await _context.Bicycles.FindAsync(rental.BicycleId);
                return View(rental);
            }

            rental.UserId = user.Id;
            rental.IsActive = true;

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return RedirectToAction("ForRent", "Bicycle");
        }
    }
}
