using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BikeShop.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BikeShop.Web.Models;
using System.Security.Claims;

namespace BikeShop.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            var rentals = await _context.Rentals
                .Include(r => r.Bicycle)
                .Where(r => r.UserId == user.Id)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();

            var orders = await _context.Orders
                .Include(o => o.Bicycle)
                .Include(o => o.Accessory) // ✅ ЕТО ТОВА ДОБАВИ!
                .Where(o => o.UserId == user.Id)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            ViewBag.Rentals = rentals;
            ViewBag.Orders = orders;

            return View(user);
        }

        public async Task<IActionResult> AccessoryOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var accessoryOrders = await _context.Orders
                .Include(o => o.Accessory)
                .Where(o => o.UserId == userId && o.AccessoryId != null)
                .ToListAsync();

            return View(accessoryOrders);
        }


    }
}
