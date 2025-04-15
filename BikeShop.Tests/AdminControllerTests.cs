
using BikeShop.Web.Controllers;
using BikeShop.Web.Data;
using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.Tests
{
    public class AdminControllerTests
    {
        private ApplicationDbContext _context;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private AdminController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AdminUserTestsDb")
                .Options;

            _context = new ApplicationDbContext(options);

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object,
                null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object,
                null, null, null, null);

            _controller = new AdminController(_userManagerMock.Object, _roleManagerMock.Object, _context);
        }

        [Test]
        public async Task Users_ShouldReturnAllUsersWithRoles()
        {
            var testUsers = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "admin1@test.com" },
                new ApplicationUser { Id = "2", UserName = "user1@test.com" }
            };

            _userManagerMock.Setup(u => u.Users).Returns(testUsers.AsQueryable());
            _userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<ApplicationUser>()))
                            .ReturnsAsync(new List<string> { "User" });

            var result = await _controller.Users();

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            var model = view.Model as List<ApplicationUser>;
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public async Task ChangeRole_ShouldUpdateUserRoles()
        {
            var user = new ApplicationUser { Id = "1", UserName = "test@test.com" };

            _userManagerMock.Setup(u => u.FindByIdAsync("1")).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });
            _userManagerMock.Setup(u => u.RemoveFromRolesAsync(user, It.IsAny<IEnumerable<string>>()))
                            .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(u => u.AddToRoleAsync(user, "Admin")).ReturnsAsync(IdentityResult.Success);

            var result = await _controller.ChangeRole("1", "Admin");

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirect = result as RedirectToActionResult;
            Assert.AreEqual("Users", redirect.ActionName);
        }

        [Test]
        public async Task AllOrders_ShouldReturnOnlyBicycleOrders()
        {
            _context.Orders.Add(new Order
            {
                Id = 1,
                UserId = "user1",
                BicycleId = 2,
                AccessoriesId = null,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.Now
            });

            _context.Orders.Add(new Order
            {
                Id = 2,
                UserId = "user2",
                AccessoriesId = 3,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            var result = await _controller.AllOrders();

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            var orders = view.Model as System.Collections.IEnumerable;

            Assert.That(orders.Cast<Order>().All(o => o.AccessoriesId == null));
        }

        [Test]
        public async Task AllAccessoriesOrders_ShouldReturnOnlyAccessoryOrders()
        {
            _context.Orders.Add(new Order
            {
                Id = 10,
                UserId = "user1",
                AccessoriesId = 4,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.Now
            });

            _context.Orders.Add(new Order
            {
                Id = 11,
                UserId = "user2",
                BicycleId = 5,
                AccessoriesId = null,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            var result = await _controller.AllAccessoriesOrders();

            Assert.IsInstanceOf<ViewResult>(result);
            var view = result as ViewResult;
            var model = view.Model as System.Collections.IEnumerable;

            Assert.That(model.Cast<Order>().All(o => o.AccessoriesId != null));
        }
    }
}
