using BikeShop.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BikeShop.Web.Data; // За seed методите, ако са в Data папката
using BikeShop.Web.Data.Seed.Bicycles;

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
        public DbSet<Accessories> Accessories { get; set; }
        public DbSet<BicycleImage> BicycleImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed-ване на велосипеди за покупка
            BuyBicycleSeed.Seed(modelBuilder);

            // Seed-ване на велосипеди за наем
            RentBicycleSeed.Seed(modelBuilder);

            //seed-ване на аксесуари
            AccessoriesSeed.Seed(modelBuilder);


            // Seed-ване на снимки към велосипедите
            BicycleImageSeed.Seed(modelBuilder);

        }
    }
}
