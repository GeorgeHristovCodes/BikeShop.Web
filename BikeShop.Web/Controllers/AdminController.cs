using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

       

        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();

            var userRoles = new Dictionary<string, IList<string>>();

            foreach (var user in users)
            {
                userRoles[user.Id] = await _userManager.GetRolesAsync(user);
            }

            ViewBag.UserRoles = userRoles;

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, newRole);
            }

            return RedirectToAction(nameof(Users));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllRentals()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Bicycle)
                .Include(r => r.User)
                .ToListAsync();

            return View(rentals);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Bicycle)
                .Include(o => o.User)
                .ToListAsync();

            return View(orders);
        }
        public async Task<IActionResult> AllAccessoryOrders()
        {
            var accessoryOrders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Accessory)
                .Where(o => o.AccessoryId != null)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(accessoryOrders);
        }


    }


}
