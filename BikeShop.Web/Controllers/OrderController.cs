using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BikeShop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Order/Create/5
        public IActionResult Create(int id)
        {
            var bicycle = _context.Bicycles.FirstOrDefault(b => b.Id == id && b.Type == BicycleType.ForSale);

            if (bicycle == null)
            {
                return NotFound();
            }

            ViewBag.Bicycle = bicycle;

            var order = new Order
            {
                BicycleId = bicycle.Id,
                TotalPrice = bicycle.Price
            };

            return View(order);
        }

        // POST: /Order/Create
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Bicycle = _context.Bicycles.FirstOrDefault(b => b.Id == order.BicycleId);
                return View(order);
            }

            order.UserId = user.Id;
            order.OrderDate = DateTime.Now;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("ForSale", "Bicycle");
        }
    }
}
