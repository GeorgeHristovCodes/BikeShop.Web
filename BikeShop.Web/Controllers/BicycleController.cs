using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BikeShop.Web.Controllers
{
    public class BicycleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BicycleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // /Bicycle/ForSale
        public async Task<IActionResult> ForSale(BicycleCategory? category)
        {
            var bicycles = _context.Bicycles
                .Where(b => b.Type == BicycleType.ForSale && b.IsAvailable);

            if (category != null)
            {
                bicycles = bicycles.Where(b => b.Category == category);
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(BicycleCategory)));
            return View(await bicycles.ToListAsync());
        }

        // /Bicycle/ForRent
        public async Task<IActionResult> ForRent(BicycleCategory? category)
        {
            var bicycles = _context.Bicycles
                .Where(b => b.Type == BicycleType.ForRent && b.IsAvailable);

            if (category != null)
            {
                bicycles = bicycles.Where(b => b.Category == category);
            }

            return View(await bicycles.ToListAsync());
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage(string? filterType, BicycleCategory? filterCategory)
        {
            var bicycles = _context.Bicycles.AsQueryable();

            if (!string.IsNullOrEmpty(filterType))
            {
                if (filterType == "ForRent")
                    bicycles = bicycles.Where(b => b.Type == BicycleType.ForRent);
                else if (filterType == "ForSale")
                    bicycles = bicycles.Where(b => b.Type == BicycleType.ForSale);
            }

            if (filterCategory != null)
            {
                bicycles = bicycles.Where(b => b.Category == filterCategory);
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(BicycleCategory)));
            return View(await bicycles.ToListAsync());
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(BicycleCategory)));
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bicycle bicycle)
        {
            if (ModelState.IsValid)
            {
                if (bicycle.ImageFile != null)
                {
                    string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(wwwRootPath))
                    {
                        Directory.CreateDirectory(wwwRootPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(bicycle.ImageFile.FileName);
                    string filePath = Path.Combine(wwwRootPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await bicycle.ImageFile.CopyToAsync(stream);
                    }

                    bicycle.ImageUrl = "/images/" + fileName;
                }

                _context.Add(bicycle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }

            return View(bicycle);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bicycles == null)
            {
                return NotFound();
            }

            var bicycle = await _context.Bicycles.FindAsync(id);
            if (bicycle == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(BicycleCategory)), bicycle.Category);
            return View(bicycle);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bicycle bicycle)
        {
            if (id != bicycle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bicycle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Bicycles.Any(e => e.Id == bicycle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Manage));
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(BicycleCategory)), bicycle.Category); // <- при невалиден модел
            return View(bicycle);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bicycles == null)
            {
                return NotFound();
            }

            var bicycle = await _context.Bicycles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bicycle == null)
            {
                return NotFound();
            }

            return View(bicycle);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bicycle = await _context.Bicycles.FindAsync(id);
            if (bicycle != null)
            {
                _context.Bicycles.Remove(bicycle);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Manage));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bicycles == null)
            {
                return NotFound();
            }

            var bicycle = await _context.Bicycles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bicycle == null)
            {
                return NotFound();
            }

            return View(bicycle);
        }




    }
}
