using BikeShop.Web.Controllers;
using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using BikeShop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Tests
{
    public class AccessoriesControllerTests
    {
        private ApplicationDbContext _context;
        private AccessoriesController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // <- ключовото
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new AccessoriesController(_context);
        }

        [Test]
        public void Index_ShouldReturnAllAccessories_WithValidData()
        {
            // Arrange – добавяме аксесоари с всички нужни полета
            _context.Accessories.AddRange(
                new Accessories
                {
                    Id = 1,
                    Name = "Helmet",
                    Price = 60,
                    Category = AccessoriesCategory.Helmet,
                    Brand = "TestBrand",
                    Stock = 5
                },
                new Accessories
                {
                    Id = 2,
                    Name = "Gloves",
                    Price = 25,
                    Category = AccessoriesCategory.Gloves,
                    Brand = "TestBrand",
                    Stock = 10
                }
            );
            _context.SaveChanges();

            var filter = new AccessoriesFilterViewModel(); // без филтри

            // Act
            var result = _controller.Index(filter) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Result is null");
            Assert.IsInstanceOf<ViewResult>(result);

            var model = result.Model as AccessoriesFilterViewModel;
            Assert.IsNotNull(model, "Model is null");

            Assert.That(model.Results.Count, Is.EqualTo(2));
            Assert.That(model.Results.Any(a => a.Name == "Helmet"));
            Assert.That(model.Results.Any(a => a.Name == "Gloves"));
        }




        [Test]
        public async Task Details_ShouldReturnAccessory_WhenExists()
        {
            // Arrange
            var accessory = new Accessories
            {
                Id = 1000,
                Name = "Pump",
                Price = 35,
                Brand = "TestBrand",
                Stock = 10,
                Category = AccessoriesCategory.Pump
            };

            _context.Accessories.Add(accessory);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.Details(1000);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            Assert.IsInstanceOf<ViewResult>(result, "Result is not a ViewResult");

            var view = result as ViewResult;
            Assert.IsNotNull(view.Model, "Model is null");

            var model = view.Model as Accessories;
            Assert.IsNotNull(model, "Accessory model is null");
            Assert.AreEqual("Pump", model.Name);
            Assert.AreEqual(1000, model.Id);
        }

        [Test]
        public async Task Edit_ShouldUpdateAccessory()
        {
            var acc = new Accessories
            {
                Id = 20,
                Name = "Knee Pads",
                Price = 45,
                Stock = 5,
                Category = AccessoriesCategory.KneePads,
                Brand = "TestBrand"
            };
            _context.Accessories.Add(acc);
            await _context.SaveChangesAsync();

            var updated = new Accessories
            {
                Id = 20,
                Name = "Updated Pads",
                Price = 50,
                Stock = 10,
                Category = AccessoriesCategory.KneePads,
                Brand = "UpdatedBrand"
            };

            var result = await _controller.Edit(20, updated, null);
            var fromDb = await _context.Accessories.FindAsync(20);

            Assert.That(fromDb.Name, Is.EqualTo("Updated Pads"));
            Assert.That(fromDb.Price, Is.EqualTo(50));
            Assert.That(fromDb.Stock, Is.EqualTo(10));
            Assert.That(fromDb.Brand, Is.EqualTo("UpdatedBrand"));
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public async Task DeleteConfirmed_ShouldRemoveAccessory_NoToken()
        {
            // Arrange
            var acc = new Accessories
            {
                Id = 111,
                Name = "Delete Test",
                Price = 10,
                Category = AccessoriesCategory.Tools,
                Stock = 1,
                Brand = "TestBrand"
            };
            _context.Accessories.Add(acc);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteConfirmed(111);

            // Assert
            var exists = await _context.Accessories.FindAsync(111);
            Assert.IsNull(exists); // аксесоарът трябва да е изтрит
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }



    }
}
