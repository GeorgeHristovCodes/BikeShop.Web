// BicycleController.cs - с логика за качване, редакция и изтриване на снимки
using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using BikeShop.Web.Models.Requests;

namespace BikeShop.Web.Controllers
{
    public class BicycleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BicycleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ForSale(BicycleFilterViewModel filter)
        {
            var query = _context.Bicycles
                .Include(b => b.Images)
                .Where(b => b.Type == BicycleType.ForSale && b.IsAvailable);

            if (filter.Category.HasValue)
                query = query.Where(b => b.Category == filter.Category.Value);

            if (!string.IsNullOrWhiteSpace(filter.Brand))
                query = query.Where(b => b.Brand == filter.Brand);

            if (!string.IsNullOrWhiteSpace(filter.FrameSize))
                query = query.Where(b => b.FrameSize == filter.FrameSize);

            if (filter.MinPrice.HasValue)
                query = query.Where(b => b.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(b => b.Price <= filter.MaxPrice.Value);

            filter.Results = query.ToList();
            filter.AvailableBrands = new List<string> { "DRAG", "NS BIKES", "SPECIALIZED", "YT INDUSTRIES" };
            filter.FrameSizes = new List<string> { "XS", "S", "M", "L", "XL", "XXL" };

            return View(filter);
        }

        [HttpGet]
        public IActionResult ForRent(BicycleFilterViewModel filter)
        {
            var query = _context.Bicycles
                .Include(b => b.Images)
                .Where(b => b.Type == BicycleType.ForRent && b.IsAvailable);

            if (filter.Category.HasValue)
                query = query.Where(b => b.Category == filter.Category.Value);

            if (!string.IsNullOrWhiteSpace(filter.Brand))
                query = query.Where(b => b.Brand == filter.Brand);

            if (!string.IsNullOrWhiteSpace(filter.FrameSize))
                query = query.Where(b => b.FrameSize == filter.FrameSize);

            if (filter.MinPrice.HasValue)
                query = query.Where(b => b.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(b => b.Price <= filter.MaxPrice.Value);

            filter.Results = query.ToList();
            filter.AvailableBrands = new List<string> { "DRAG", "NS BIKES", "SPECIALIZED", "YT INDUSTRIES" };
            filter.FrameSizes = new List<string> { "XS", "S", "M", "L", "XL", "XXL" };

            return View(filter);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Manage(string? filterType, BicycleCategory? filterCategory)
        {
            var bicycles = _context.Bicycles.AsQueryable();

            if (!string.IsNullOrEmpty(filterType))
            {
                bicycles = bicycles.Where(b => b.Type.ToString() == filterType);
            }

            if (filterCategory.HasValue)
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
                    if (!Directory.Exists(wwwRootPath)) Directory.CreateDirectory(wwwRootPath);

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

                foreach (var image in Request.Form.Files.Where(f => f.Name == "AdditionalImages"))
                {
                    if (image.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bicycles", fileName);
                        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        _context.BicycleImages.Add(new BicycleImage
                        {
                            BicycleId = bicycle.Id,
                            ImageUrl = "/images/bicycles/" + fileName
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(BicycleCategory)), bicycle.Category);
            return View(bicycle);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var bicycle = await _context.Bicycles.Include(b => b.Images).FirstOrDefaultAsync(b => b.Id == id);
            if (bicycle == null) return NotFound();

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(BicycleCategory)), bicycle.Category);
            return View(bicycle);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bicycle bicycle)
        {
            if (id != bicycle.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingBike = await _context.Bicycles.Include(b => b.Images).FirstOrDefaultAsync(b => b.Id == id);
                if (existingBike == null) return NotFound();

                if (bicycle.ImageFile != null)
                {
                    var root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(root)) Directory.CreateDirectory(root);

                    if (!string.IsNullOrEmpty(existingBike.ImageUrl))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBike.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                    }

                    var fileName = Guid.NewGuid() + Path.GetExtension(bicycle.ImageFile.FileName);
                    var filePath = Path.Combine(root, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await bicycle.ImageFile.CopyToAsync(stream);
                    }

                    existingBike.ImageUrl = "/images/" + fileName;
                }

                foreach (var image in Request.Form.Files.Where(f => f.Name == "AdditionalImages"))
                {
                    if (image.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bicycles", fileName);
                        Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        _context.BicycleImages.Add(new BicycleImage
                        {
                            BicycleId = existingBike.Id,
                            ImageUrl = "/images/bicycles/" + fileName
                        });
                    }
                }

                existingBike.Name = bicycle.Name;
                existingBike.Description = bicycle.Description;
                existingBike.Price = bicycle.Price;
                existingBike.Type = bicycle.Type;
                existingBike.Category = bicycle.Category;
                existingBike.Brand = bicycle.Brand;
                existingBike.FrameSize = bicycle.FrameSize;
                existingBike.Quantity = bicycle.Quantity;
                existingBike.IsAvailable = bicycle.IsAvailable;

                _context.Update(existingBike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(BicycleCategory)), bicycle.Category);
            return View(bicycle);
        }

      
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken] // 👈 Ако имаш този ред, JS-а трябва да подаде валиден анти-фалшификация токен
        public async Task<IActionResult> DeleteImage([FromBody] DeleteImageRequest request)

        {
            var image = await _context.BicycleImages.FindAsync(request.ImageId);
            if (image == null) return NotFound();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            _context.BicycleImages.Remove(image);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var bicycle = await _context.Bicycles.FirstOrDefaultAsync(m => m.Id == id);
            if (bicycle == null) return NotFound();

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
            if (id == null) return NotFound();

            var bicycle = await _context.Bicycles.Include(b => b.Images).FirstOrDefaultAsync(m => m.Id == id);
            if (bicycle == null) return NotFound();

            return View(bicycle);
        }
    }
}
