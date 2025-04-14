using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

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
                .Where(o => o.AccessoriesId == null) // само поръчки на велосипеди
                .ToListAsync();

            return View(orders);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllAccessoriesOrders()
        {
            var AccessoriesOrders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Accessories)
                .Where(o => o.AccessoriesId != null)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(AccessoriesOrders);
        }

        // ✅ Приемане/отказване на поръчки за велосипеди
        [HttpPost]
        public async Task<IActionResult> AcceptOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = OrderStatus.Accepted;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllOrders));
        }

        [HttpPost]
        public async Task<IActionResult> RejectOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = OrderStatus.Rejected;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllOrders));
        }

        // ✅ Приемане/отказване на наеми
        [HttpPost]
        public async Task<IActionResult> AcceptRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return NotFound();

            rental.Status = OrderStatus.Accepted;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllRentals));
        }

        [HttpPost]
        public async Task<IActionResult> RejectRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return NotFound();

            rental.Status = OrderStatus.Rejected;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllRentals));
        }

        // ✅ Приемане/отказване на поръчки за аксесоари
        [HttpPost]
        public async Task<IActionResult> AcceptAccessoriesOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null || order.AccessoriesId == null) return NotFound();

            order.Status = OrderStatus.Accepted;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllAccessoriesOrders));
        }

        [HttpPost]
        public async Task<IActionResult> RejectAccessoriesOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null || order.AccessoriesId == null) return NotFound();

            order.Status = OrderStatus.Rejected;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllAccessoriesOrders));
        }
    }
}
