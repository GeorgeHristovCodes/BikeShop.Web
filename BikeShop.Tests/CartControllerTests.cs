
using BikeShop.Web.Controllers;
using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BikeShop.Tests
{
    public class CartControllerTests_Extra
    {
        private ApplicationDbContext _context;
        private CartController _controller;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CartTestDb_Extra")
                .Options;

            _context = new ApplicationDbContext(options);

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            _controller = new CartController(_context, _userManagerMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Test]
        public void CalculateRentalPrice_ShouldReturnCorrectPrice()
        {
            var method = typeof(CartController).GetMethod("CalculateRentalPrice", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var result = (decimal)method.Invoke(_controller, new object[] { 40m, DateTime.Today, DateTime.Today.AddDays(5) });

            Assert.AreEqual(200, result);
        }

        [Test]
        public async Task Remove_ShouldReturnNotFound_IfItemDoesNotExist()
        {
            var result = await _controller.Remove(999);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CheckoutRental_ShouldRedirect_WhenCartIsEmpty()
        {
            var result = await _controller.CheckoutRental();
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            var redirect = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirect.ActionName);
        }
    }
}
