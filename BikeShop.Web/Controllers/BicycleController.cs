using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Controllers
{
    public class BicycleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BicycleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // /Bicycle/ForSale
        public async Task<IActionResult> ForSale()
        {
            var bicycles = await _context.Bicycles
                .Where(b => b.Type == BicycleType.ForSale && b.IsAvailable)
                .ToListAsync();

            return View(bicycles);
        }

        // /Bicycle/ForRent
        public async Task<IActionResult> ForRent()
        {
            var bicycles = await _context.Bicycles
                .Where(b => b.Type == BicycleType.ForRent && b.IsAvailable)
                .ToListAsync();

            return View(bicycles);
        }
    }
}
