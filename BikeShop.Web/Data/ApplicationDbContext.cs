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
        public DbSet<Accessory> Accessories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Accessory>().HasData(
                new Accessory
                {
                    Id = 1,
                    Name = "DRAG Каска PRO",
                    Description = "Лека и удобна каска с вентилационни отвори",
                    Price = 129.99m,
                    Brand = "DRAG",
                    Category = AccessoryCategory.Helmet,
                    Stock = 15,
                    ImageUrl = "/images/accessories/helmet1.jpg"
                },
                new Accessory
                {
                    Id = 2,
                    Name = "NS Rъкавици GripX",
                    Description = "Противохлъзгаща вътрешност и дишаща материя",
                    Price = 39.50m,
                    Brand = "NS BIKES",
                    Category = AccessoryCategory.Gloves,
                    Stock = 30,
                    ImageUrl = "/images/accessories/gloves1.jpg"
                },
                new Accessory
                {
                    Id = 3,
                    Name = "SPECIALIZED Помпа AirTool",
                    Description = "Удобна и здрава ръчна помпа",
                    Price = 49.00m,
                    Brand = "SPECIALIZED",
                    Category = AccessoryCategory.Pump,
                    Stock = 20,
                    ImageUrl = "/images/accessories/pump1.jpg"
                }
            );
        }

    }
}
