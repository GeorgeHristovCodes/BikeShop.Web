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
using System.Security.Claims;

namespace BikeShop.Tests
{
    public class AccountControllerTests
    {
        private ApplicationDbContext _context;
        private AccountController _controller;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            _controller = new AccountController(_context, _userManagerMock.Object);
        }

        [Test]
        public async Task Profile_ShouldReturnViewWithUser()
        {
            var user = new ApplicationUser { Id = "user-1", UserName = "test@test.com" };
            _context.Users.Add(user);

            _context.Rentals.Add(new Rental
            {
                BicycleId = 1,
                UserId = "user-1",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Price = 50,
                FirstName = "Ivan",
                LastName = "Ivanov",
                PhoneNumber = "0888123456",
                City = "Plovdiv"
            });

            _context.Orders.Add(new Order
            {
                UserId = "user-1",
                OrderDate = DateTime.Today,
                TotalPrice = 100
            });

            await _context.SaveChangesAsync();

            _userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var result = await _controller.Profile();

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            Assert.IsInstanceOf<ApplicationUser>(view.Model);
        }

        [Test]
        public async Task Profile_ShouldReturnView_WhenUserHasNoOrdersOrRentals()
        {
            var user = new ApplicationUser { Id = "user-empty", UserName = "empty@test.com" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            var result = await _controller.Profile();

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            Assert.IsInstanceOf<ApplicationUser>(view.Model);
        }
    }
}
