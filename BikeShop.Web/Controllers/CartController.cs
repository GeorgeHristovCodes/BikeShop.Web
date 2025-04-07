// ✅ ОБНОВЕН CartController.cs — с поддръжка за гост потребители и прехвърляне към акаунт
using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    private string GetCurrentUserId()
    {
        if (User.Identity.IsAuthenticated)
            return User.FindFirstValue(ClaimTypes.NameIdentifier);

        const string sessionKey = "GuestId";
        if (!HttpContext.Session.TryGetValue(sessionKey, out _))
        {
            HttpContext.Session.SetString(sessionKey, Guid.NewGuid().ToString());
        }
        return HttpContext.Session.GetString(sessionKey)!;
    }

    public async Task<IActionResult> Index()
    {
        var userId = GetCurrentUserId();

        // 🔁 Прехвърляне от Guest към акаунт
        if (User.Identity.IsAuthenticated)
        {
            var guestId = HttpContext.Session.GetString("GuestId");
            if (!string.IsNullOrEmpty(guestId) && guestId != userId)
            {
                var guestItems = await _context.CartItems.Where(c => c.UserId == guestId).ToListAsync();
                foreach (var item in guestItems)
                {
                    item.UserId = userId;
                }
                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("GuestId");
            }
        }

        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return View(cartItems);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int bicycleId, CartItemType type, DateTime? rentalStartDate = null, DateTime? rentalEndDate = null)
    {
        var userId = GetCurrentUserId();
        var bicycle = await _context.Bicycles.FindAsync(bicycleId);
        if (bicycle == null) return NotFound();

        var price = type == CartItemType.Purchase ? bicycle.Price : 0;

        var cartItem = new CartItem
        {
            BicycleId = bicycleId,
            UserId = userId,
            Type = type,
            Price = price,
            RentalStartDate = rentalStartDate,
            RentalEndDate = rentalEndDate
        };

        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item == null) return NotFound();

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> CheckoutRental()
    {
        if (!User.Identity.IsAuthenticated)
            return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = "/Cart/CheckoutRental" });

        var userId = GetCurrentUserId();
        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Where(c => c.UserId == userId && c.Type == CartItemType.Rental)
            .ToListAsync();

        if (!cartItems.Any())
        {
            TempData["Error"] = "Нямате артикули за наем в кошницата.";
            return RedirectToAction("Index");
        }

        var model = new RentalCheckoutViewModel { CartItems = cartItems };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckoutRental(RentalCheckoutViewModel model)
    {
        var userId = GetCurrentUserId();
        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Where(c => c.UserId == userId && c.Type == CartItemType.Rental)
            .ToListAsync();

        if (!ModelState.IsValid || !cartItems.Any())
        {
            model.CartItems = cartItems;
            return View(model);
        }

        foreach (var item in cartItems)
        {
            var rental = new Rental
            {
                BicycleId = item.BicycleId,
                UserId = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                StartDate = model.RentalStartDate,
                EndDate = model.RentalEndDate,
                Price = CalculateRentalPrice(item.Bicycle.Price, model.RentalStartDate, model.RentalEndDate),
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            _context.Rentals.Add(rental);
        }

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        return RedirectToAction("SuccessRental");
    }

    private decimal CalculateRentalPrice(decimal dailyPrice, DateTime start, DateTime end)
    {
        var days = (end - start).Days;
        return dailyPrice * (days > 0 ? days : 1);
    }

    public IActionResult SuccessRental() => View();

    [HttpGet]
    public async Task<IActionResult> CheckoutOrder()
    {
        if (!User.Identity.IsAuthenticated)
            return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = "/Cart/CheckoutOrder" });

        var userId = GetCurrentUserId();
        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Where(c => c.UserId == userId && c.Type == CartItemType.Purchase)
            .ToListAsync();

        if (!cartItems.Any())
        {
            TempData["Error"] = "Нямате артикули за покупка в кошницата.";
            return RedirectToAction("Index");
        }

        var model = new OrderCheckoutViewModel { CartItems = cartItems };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckoutOrder(OrderCheckoutViewModel model)
    {
        var userId = GetCurrentUserId();
        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Where(c => c.UserId == userId && c.Type == CartItemType.Purchase)
            .ToListAsync();

        if (!ModelState.IsValid || !cartItems.Any())
        {
            model.CartItems = cartItems;
            return View(model);
        }

        foreach (var item in cartItems)
        {
            var order = new Order
            {
                BicycleId = item.BicycleId,
                UserId = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                CustomerCity = model.CustomerCity,
                DeliveryCity = model.DeliveryCity,
                DeliveryStreet = model.DeliveryStreet,
                DeliveryStreetNumber = model.DeliveryStreetNumber,
                PostalCode = model.PostalCode,
                IsDelivery = model.IsDelivery,
                OrderDate = DateTime.Now,
                TotalPrice = item.Price
            };

            _context.Orders.Add(order);

            var bicycle = await _context.Bicycles.FindAsync(item.BicycleId);
            if (bicycle != null)
            {
                bicycle.Quantity--;
                if (bicycle.Quantity <= 0)
                    bicycle.IsAvailable = false;
            }
        }

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        return RedirectToAction("SuccessOrder");
    }

    public IActionResult SuccessOrder() => View();
}
