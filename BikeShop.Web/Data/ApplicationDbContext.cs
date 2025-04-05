using BikeShop.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bicycle> Bicycles { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed велосипеди за наем
            builder.Entity<Bicycle>().HasData(
                new Bicycle
                {
                    Id = 1,
                    Name = "Rent Bike 1",
                    Description = "Планински велосипед под наем",
                    Price = 15,
                    Type = BicycleType.ForRent,
                    ImageUrl = "https://via.placeholder.com/300x200",
                    IsAvailable = true
                },
                new Bicycle
                {
                    Id = 2,
                    Name = "Rent Bike 2",
                    Description = "Градски велосипед под наем",
                    Price = 10,
                    Type = BicycleType.ForRent,
                    ImageUrl = "https://via.placeholder.com/300x200",
                    IsAvailable = true
                },
                new Bicycle
                {
                    Id = 3,
                    Name = "Road Bike Pro",
                    Description = "Шосеен велосипед с карбонова рамка.",
                    Price = 1200,
                    Type = BicycleType.ForSale,
                    ImageUrl = "https://via.placeholder.com/300x200",
                    IsAvailable = true
                },
                new Bicycle
                {
                      Id = 4,
                      Name = "City Cruiser",
                      Description = "Удобен градски велосипед за ежедневна употреба.",
                      Price = 750,
                      Type = BicycleType.ForSale,
                      ImageUrl = "https://via.placeholder.com/300x200",
                      IsAvailable = true
                }
            );
        }
    }
}
