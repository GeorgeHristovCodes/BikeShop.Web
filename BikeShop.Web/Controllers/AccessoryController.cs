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

        // Всички аксесоари
        public async Task<IActionResult> Index()
        {
            var accessories = await _context.Accessories.ToListAsync();
            return View(accessories);
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
                // Копираме старите стойности, за да не загубим нищо
                existingAccessory.Name = accessory.Name;
                existingAccessory.Description = accessory.Description;
                existingAccessory.Price = accessory.Price;
                existingAccessory.Brand = accessory.Brand;
                existingAccessory.Category = accessory.Category;
                existingAccessory.Stock = accessory.Stock;

                // Ако има нова снимка – качваме я
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
