using BikeShop.Web.Controllers;
using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using BikeShop.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Diagnostics;

namespace BikeShop.Tests
{
    public class HomeControllerTests
    {
        private ApplicationDbContext _context;
        private HomeController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // уникална база за всеки тест
                .Options;

            _context = new ApplicationDbContext(options);

            var loggerMock = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(loggerMock.Object, _context);
        }

        [Test]
        public void About_ShouldReturnView()
        {
            var result = _controller.About();
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Contact_ShouldReturnView()
        {
            var result = _controller.Contact();
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Privacy_ShouldReturnView()
        {
            var result = _controller.Privacy();
            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
