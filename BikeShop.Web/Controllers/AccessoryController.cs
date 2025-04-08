using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Controllers
{
    public class AccessoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccessoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Филтрирани аксесоари
        public IActionResult Index(AccessoryFilterViewModel filter)
        {
            var query = _context.Accessories.AsQueryable();

            if (filter.Category.HasValue)
                query = query.Where(a => a.Category == filter.Category.Value);

            if (filter.MinPrice.HasValue)
                query = query.Where(a => a.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(a => a.Price <= filter.MaxPrice.Value);

            filter.Results = query.ToList();
            filter.AvailableCategories = Enum.GetValues(typeof(AccessoryCategory)).Cast<AccessoryCategory>().ToList();

            return View(filter);
        }

        // Детайли за аксесоар
        public async Task<IActionResult> Details(int id)
        {
            var accessory = await _context.Accessories.FirstOrDefaultAsync(a => a.Id == id);
            if (accessory == null) return NotFound();

            return View(accessory);
        }

        // Създаване на нов аксесоар (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // Създаване на нов аксесоар (POST)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Accessory accessory, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/accessories", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    accessory.ImageUrl = "/images/accessories/" + fileName;
                }

                _context.Add(accessory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(accessory);
        }

        // Редакция на аксесоар (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var accessory = await _context.Accessories.FindAsync(id);
            if (accessory == null) return NotFound();

            return View(accessory);
        }

        // Редакция на аксесоар (POST)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Accessory accessory, IFormFile? imageFile)
        {
            if (id != accessory.Id) return NotFound();

            var existingAccessory = await _context.Accessories.FindAsync(id);
            if (existingAccessory == null) return NotFound();

            if (ModelState.IsValid)
            {
                existingAccessory.Name = accessory.Name;
                existingAccessory.Description = accessory.Description;
                existingAccessory.Price = accessory.Price;
                existingAccessory.Brand = accessory.Brand;
                existingAccessory.Category = accessory.Category;
                existingAccessory.Stock = accessory.Stock;

                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/accessories", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    existingAccessory.ImageUrl = "/images/accessories/" + fileName;
                }

                _context.Update(existingAccessory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(accessory);
        }

        // Изтриване на аксесоар (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var accessory = await _context.Accessories.FindAsync(id);
            if (accessory == null) return NotFound();

            return View(accessory);
        }

        // Изтриване на аксесоар (POST)
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessory = await _context.Accessories.FindAsync(id);
            if (accessory != null)
            {
                _context.Accessories.Remove(accessory);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Админ панел
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage(AccessoryCategory? category = null)
        {
            var accessories = _context.Accessories.AsQueryable();

            if (category.HasValue)
            {
                accessories = accessories.Where(a => a.Category == category.Value);
            }

            var model = await accessories.ToListAsync();
            ViewBag.SelectedCategory = category;
            return View(model);
        }
    }
}