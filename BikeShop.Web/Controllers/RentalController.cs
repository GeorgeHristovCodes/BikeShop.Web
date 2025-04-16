using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Rental rental)
        {
            rental.Id = 0;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            rental.UserId = user.Id;
            ModelState.Remove("UserId");

            var bicycle = await _context.Bicycles.FindAsync(rental.BicycleId);
            if (bicycle == null || !bicycle.IsAvailable)
            {
                return BadRequest("Велосипедът не е наличен");
            }

            await _context.Entry(bicycle).ReloadAsync(); // <-- актуални данни от БД

            if (bicycle.Quantity <= 0)
            {
                TempData["Error"] = $"Няма налични бройки от '{bicycle.Name}'.";
                return RedirectToAction("Create", new { id = rental.BicycleId });
            }

            if (rental.StartDate >= rental.EndDate)
            {
                ModelState.AddModelError("", "Крайната дата трябва да е след началната.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Bicycle = bicycle;
                return View(rental);
            }

            rental.IsActive = true;
            rental.CreatedOn = DateTime.Now;

            _context.Rentals.Add(rental);
            bicycle.Quantity--;

            if (bicycle.Quantity == 0)
                bicycle.IsAvailable = false;

            await _context.SaveChangesAsync();

            TempData["Success"] = "✅ Велосипедът е успешно нает.";
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
