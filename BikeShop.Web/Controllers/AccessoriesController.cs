using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using BikeShop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccessoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Филтрирани аксесоари
        public IActionResult Index(AccessoriesFilterViewModel filter)
        {
            var query = _context.Accessories.AsQueryable();

            if (filter.Category.HasValue)
                query = query.Where(a => a.Category == filter.Category.Value);

            if (filter.MinPrice.HasValue)
                query = query.Where(a => a.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(a => a.Price <= filter.MaxPrice.Value);

            filter.Results = query.ToList();
            filter.AvailableCategories = Enum.GetValues(typeof(AccessoriesCategory)).Cast<AccessoriesCategory>().ToList();

            return View(filter);
        }

        // Детайли за аксесоар

        public async Task<IActionResult> Details(int id)
        {
            var Accessories = await _context.Accessories.FirstOrDefaultAsync(a => a.Id == id);
            if (Accessories == null) return NotFound();

            return View(Accessories);
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
        public async Task<IActionResult> Create(Accessories Accessories, IFormFile imageFile)
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

                    Accessories.ImageUrl = "/images/accessories/" + fileName;
                }

                _context.Add(Accessories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(Accessories);
        }

        // Редакция на аксесоар (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var Accessories = await _context.Accessories.FindAsync(id);
            if (Accessories == null) return NotFound();

            return View(Accessories);
        }

        // Редакция на аксесоар (POST)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Accessories Accessories, IFormFile? imageFile)
        {
            if (id != Accessories.Id) return NotFound();

            var existingAccessories = await _context.Accessories.FindAsync(id);
            if (existingAccessories == null) return NotFound();

            if (ModelState.IsValid)
            {
                existingAccessories.Name = Accessories.Name;
                existingAccessories.Description = Accessories.Description;
                existingAccessories.Price = Accessories.Price;
                existingAccessories.Brand = Accessories.Brand;
                existingAccessories.Category = Accessories.Category;
                existingAccessories.Stock = Accessories.Stock;

                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/accessories", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    existingAccessories.ImageUrl = "/images/accessories/" + fileName;
                }

                _context.Update(existingAccessories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(Accessories);
        }

        // Изтриване на аксесоар (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var Accessories = await _context.Accessories.FindAsync(id);
            if (Accessories == null) return NotFound();

            return View(Accessories);
        }

        // Изтриване на аксесоар (POST)
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Accessories = await _context.Accessories.FindAsync(id);
            if (Accessories != null)
            {
                _context.Accessories.Remove(Accessories);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Админ панел
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage(AccessoriesCategory? category = null)
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