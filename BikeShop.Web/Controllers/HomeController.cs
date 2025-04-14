using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BikeShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var forSale = _context.Bicycles
                 .Where(b => b.Type == BicycleType.ForSale)
                 .ToList() // 👈 Взимаме всичко в паметта
                .GroupBy(b => b.Category)
                .Select(g => g.First())
                .Take(3)
                .Select(b => new Bicycle
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price
                }).ToList();

            var forRent = _context.Bicycles
                .Where(b => b.Type == BicycleType.ForRent)
                .ToList() // 👈 Взимаме всичко в паметта
                .GroupBy(b => b.Category)
                .Select(g => g.First())
                .Take(3)
                .Select(b => new Bicycle
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Price = b.Price,
                    Brand = b.Brand,
                    FrameSize = b.FrameSize,
                    Category = b.Category
                }).ToList();

            var model = new HomeViewModel
            {
                BicyclesForSale = forSale,
                BicyclesForRent = forRent
            };

            return View(model);
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
