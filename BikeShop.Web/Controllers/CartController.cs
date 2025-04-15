using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Include(c => c.Accessories)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return View(cartItems);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int bicycleId, CartItemType type, string? returnUrl)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var bicycle = await _context.Bicycles.FindAsync(bicycleId);

        if (bicycle == null)
            return NotFound();

        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.BicycleId == bicycleId && c.UserId == userId && c.Type == type);

        if (existingItem != null)
        {
            existingItem.Quantity++;
            existingItem.Price = existingItem.Quantity * bicycle.Price;
        }
        else
        {
            var price = type == CartItemType.Purchase ? bicycle.Price : 0;

            var cartItem = new CartItem
            {
                BicycleId = bicycleId,
                UserId = userId,
                Type = type,
                Quantity = 1,
                Price = price
            };

            _context.CartItems.Add(cartItem);
        }

        await _context.SaveChangesAsync();

        TempData["Success"] = "✅ Артикулът беше добавен в кошницата.";
        return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Index", "Cart");
    }



    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();

        TempData["Success"] = "🗑️ Артикулът беше премахнат от кошницата.";

        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> CheckoutRental()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Where(c => c.UserId == userId && c.Type == CartItemType.Rental)
            .ToListAsync();

        if (!cartItems.Any())
        {
            return RedirectToAction("Index");
        }

        var model = new RentalCheckoutViewModel
        {
            CartItems = cartItems,
            RentalStartDate = DateTime.Now.Date,
            RentalEndDate = DateTime.Now.Date.AddDays(1)
        };

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckoutRental(RentalCheckoutViewModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
                BicycleId = item.BicycleId ?? 0,
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

        TempData["Success"] = "✅ Успешен наем!";
        return RedirectToAction("SuccessRental");
    }

    private decimal CalculateRentalPrice(decimal dailyPrice, DateTime start, DateTime end)
    {
        var days = (end - start).Days;
        return dailyPrice * (days > 0 ? days : 1);
    }

    public IActionResult SuccessRental()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CheckoutOrder()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Include(c => c.Accessories)
            .Where(c => c.UserId == userId && c.Type == CartItemType.Purchase)
            .ToListAsync();

        if (!cartItems.Any())
        {
            return RedirectToAction("Index");
        }

        var model = new OrderCheckoutViewModel
        {
            CartItems = cartItems
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckoutOrder(OrderCheckoutViewModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.CartItems
            .Include(c => c.Bicycle)
            .Include(c => c.Accessories)
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

            if (item.BicycleId > 0)
            {
                order.BicycleId = item.BicycleId;

                var bicycle = await _context.Bicycles.FindAsync(item.BicycleId);
                if (bicycle != null)
                {
                    bicycle.Quantity--;
                    if (bicycle.Quantity <= 0)
                        bicycle.IsAvailable = false;
                }
            }
            else if (item.AccessoriesId > 0)
            {
                order.AccessoriesId = item.AccessoriesId;

                var Accessories = await _context.Accessories.FindAsync(item.AccessoriesId);
                if (Accessories != null)
                {
                    Accessories.Stock -= item.Quantity;
                    if (Accessories.Stock < 0)
                        Accessories.Stock = 0;
                }
            }

            _context.Orders.Add(order);
        }

        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        TempData["Success"] = "✅ Поръчката е успешно направена.";
        return RedirectToAction("SuccessOrder");
    }

    public IActionResult SuccessOrder()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAccessories(int AccessoriesId)
    {
        var Accessories = await _context.Accessories.FindAsync(AccessoriesId);

        if (Accessories == null || Accessories.Stock <= 0)
        {
            return RedirectToAction("Index", "Accessories");
        }

        var userId = _userManager.GetUserId(User);

        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId
                                   && c.AccessoriesId == AccessoriesId
                                   && c.Type == CartItemType.Purchase);

        if (existingItem == null)
        {
            var cartItem = new CartItem
            {
                UserId = userId,
                AccessoriesId = Accessories.Id,
                Quantity = 1,
                Type = CartItemType.Purchase,
                Price = Accessories.Price
            };

            _context.CartItems.Add(cartItem);
        }
        else
        {
            existingItem.Quantity++;
            existingItem.Price += Accessories.Price;
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "✅ Аксесоарът беше добавен в количката.";
        return RedirectToAction("Index", "Accessories");
    }
}
