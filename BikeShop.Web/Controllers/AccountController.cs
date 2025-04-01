using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BikeShop.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BikeShop.Web.Models;

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

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            var myRentals = await _context.Rentals
                .Include(r => r.Bicycle)
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            var myOrders = await _context.Orders
                .Include(o => o.Bicycle)
                .Where(o => o.UserId == user.Id)
                .ToListAsync();

            ViewBag.MyRentals = myRentals;
            ViewBag.MyOrders = myOrders;

            return View();
        }
    }
}
