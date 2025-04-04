﻿using BikeShop.Web.Data;
using BikeShop.Web.Models;
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

        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Bicycles
                .Where(b => b.Type == BicycleType.ForRent && b.IsAvailable)
                .Take(2)
                .ToListAsync();

            var sales = await _context.Bicycles
                .Where(b => b.Type == BicycleType.ForSale && b.IsAvailable)
                .Take(2)
                .ToListAsync();

            ViewBag.Rentals = rentals;
            ViewBag.Sales = sales;

            return View();
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
