
using BikeShop.Web.Controllers;
using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using BikeShop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Tests
{
    public class BicycleControllerTests_Corrected
    {
        private ApplicationDbContext _context;
        private BicycleController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BicycleControllerCorrectedDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new BicycleController(_context);
        }

        [Test]
        public void ForSale_ShouldReturnViewResult()
        {
            var bike = new Bicycle
            {
                Id = 1,
                Name = "Sale Bike",
                IsAvailable = true,
                Type = BicycleType.ForSale,
                Price = 500,
                Category = BicycleCategory.City,
                Brand = "DRAG",
                FrameSize = "M"
            };
            _context.Bicycles.Add(bike);
            _context.SaveChanges();

            var filter = new BicycleFilterViewModel
            {
                Brand = "DRAG",
                FrameSize = "M",
                Category = BicycleCategory.City,
                MinPrice = 100,
                MaxPrice = 600
            };

            var result = _controller.ForSale(filter) as ViewResult;
            var model = result.Model as BicycleFilterViewModel;

            Assert.IsNotNull(model);
            Assert.That(model.Results.Count, Is.EqualTo(1));
            Assert.That(model.Results.First().Name, Is.EqualTo("Sale Bike"));
        }

        [Test]
        public void ForRent_ShouldReturnViewResult()
        {
            var bike = new Bicycle
            {
                Id = 2,
                Name = "Rent Bike",
                IsAvailable = true,
                Type = BicycleType.ForRent,
                Price = 300,
                Category = BicycleCategory.MountainFullSuspension,
                Brand = "NS BIKES",
                FrameSize = "L"
            };
            _context.Bicycles.Add(bike);
            _context.SaveChanges();

            var filter = new BicycleFilterViewModel
            {
                Brand = "NS BIKES",
                FrameSize = "L",
                Category = BicycleCategory.MountainFullSuspension,
                MinPrice = 200,
                MaxPrice = 400
            };

            var result = _controller.ForRent(filter) as ViewResult;
            var model = result.Model as BicycleFilterViewModel;

            Assert.IsNotNull(model);
            Assert.That(model.Results.Count, Is.EqualTo(1));
            Assert.That(model.Results.First().Name, Is.EqualTo("Rent Bike"));
        }

        [Test]
        public async Task Details_ShouldReturnNotFound_WhenIdIsInvalid()
        {
            var result = await _controller.Details(null);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
