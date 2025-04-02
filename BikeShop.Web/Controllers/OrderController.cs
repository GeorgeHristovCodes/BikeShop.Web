using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Create(int id)
        {
            var bicycle = await _context.Bicycles.FindAsync(id);
            if (bicycle == null || bicycle.Type != BicycleType.ForSale)
                return NotFound();

            ViewBag.Bicycle = bicycle;

            var order = new Order
            {
                BicycleId = id,
                TotalPrice = bicycle.Price
            };

            return View(order);
        }

        // POST: /Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            order.Id = 0;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            order.UserId = user.Id;
            ModelState.Remove("UserId");

            var bicycle = await _context.Bicycles.FindAsync(order.BicycleId);
            if (bicycle == null || bicycle.Quantity <= 0)
                return BadRequest("Велосипедът не е наличен");

            if (!ModelState.IsValid)
            {
                ViewBag.Bicycle = bicycle;
                return View(order);
            }

            order.TotalPrice = bicycle.Price;
            order.OrderDate = DateTime.Now;

            _context.Orders.Add(order);

            bicycle.Quantity--;
            if (bicycle.Quantity == 0)
                bicycle.IsAvailable = false;

            await _context.SaveChangesAsync();

            return RedirectToAction("Success");



        }
        public IActionResult Success()
        {
            return View();
        }

    }
}
