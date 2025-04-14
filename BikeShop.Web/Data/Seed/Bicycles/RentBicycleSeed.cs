using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Data
{
    public static class RentBicycleSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bicycle>().HasData(
                new Bicycle
                {
                    Id = 11,
                    Name = "Drag X-Rent Road",
                    Description = "Шосеен велосипед за наем с алуминиева рамка, лек и комфортен.",
                    Price = 25,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.Road,
                    Brand = "DRAG",
                    FrameSize = "M",
                    ImageUrl = "/images/demo-bicycles/road-rent1.jpg",
                    IsAvailable = true,
                    Quantity = 3
                },
                new Bicycle
                {
                    Id = 12,
                    Name = "Specialized Allez Rent",
                    Description = "Шосейно колело за бързо придвижване в града и околностите.",
                    Price = 30,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.Road,
                    Brand = "SPECIALIZED",
                    FrameSize = "L",
                    ImageUrl = "/images/demo-bicycles/road-rent2.jpg",
                    IsAvailable = true,
                    Quantity = 4
                },

                new Bicycle
                {
                    Id = 13,
                    Name = "NS Rent Zircus",
                    Description = "Хардтейл колело за планинско каране и скокове – перфектно за наем.",
                    Price = 35,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.MountainHardtail,
                    Brand = "NS BIKES",
                    FrameSize = "S",
                    ImageUrl = "/images/demo-bicycles/hardtail-rent1.jpg",
                    IsAvailable = true,
                    Quantity = 5
                },
                new Bicycle
                {
                    Id = 14,
                    Name = "Drag Hardy Rent",
                    Description = "Здрав велосипед под наем, идеален за офроуд разходки в планината.",
                    Price = 38,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.MountainHardtail,
                    Brand = "DRAG",
                    FrameSize = "M",
                    ImageUrl = "/images/demo-bicycles/hardtail-rent2.jpg",
                    IsAvailable = true,
                    Quantity = 6
                },

                new Bicycle
                {
                    Id = 15,
                    Name = "YT Jeffsy Rent",
                    Description = "Пълноокачен велосипед за под наем с максимално удобство по тежък терен.",
                    Price = 45,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.MountainFullSuspension,
                    Brand = "YT INDUSTRIES",
                    FrameSize = "L",
                    ImageUrl = "/images/demo-bicycles/full-rent1.jpg",
                    IsAvailable = true,
                    Quantity = 3
                },
                new Bicycle
                {
                    Id = 16,
                    Name = "Stumpjumper Evo Rent",
                    Description = "Професионален MTB за сериозни трасета, достъпен под наем.",
                    Price = 48,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.MountainFullSuspension,
                    Brand = "SPECIALIZED",
                    FrameSize = "XL",
                    ImageUrl = "/images/demo-bicycles/full-rent2.jpg",
                    IsAvailable = true,
                    Quantity = 2
                },

                new Bicycle
                {
                    Id = 17,
                    Name = "Drag City Rent",
                    Description = "Градски велосипед под наем за комфортно придвижване.",
                    Price = 20,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.City,
                    Brand = "DRAG",
                    FrameSize = "M",
                    ImageUrl = "/images/demo-bicycles/city-rent1.jpg",
                    IsAvailable = true,
                    Quantity = 5
                },
                new Bicycle
                {
                    Id = 18,
                    Name = "NS City Urban Rent",
                    Description = "Стилен градски велосипед, удобен за кратки наеми.",
                    Price = 22,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.City,
                    Brand = "NS BIKES",
                    FrameSize = "L",
                    ImageUrl = "/images/demo-bicycles/city-rent2.jpg",
                    IsAvailable = true,
                    Quantity = 4
                },

                new Bicycle
                {
                    Id = 19,
                    Name = "Drag Kiddo Rent 20\"",
                    Description = "Безопасен детски велосипед за наем, подходящ за деца 5-8г.",
                    Price = 12,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.Kids,
                    Brand = "DRAG",
                    FrameSize = "XS",
                    ImageUrl = "/images/demo-bicycles/kid-rent1.jpg",
                    IsAvailable = true,
                    Quantity = 4
                },
                new Bicycle
                {
                    Id = 20,
                    Name = "YT Primus Rent 24\"",
                    Description = "Детски велосипед за деца 8-12г. Подходящ за разходки в парка.",
                    Price = 14,
                    Type = BicycleType.ForRent,
                    Category = BicycleCategory.Kids,
                    Brand = "YT INDUSTRIES",
                    FrameSize = "S",
                    ImageUrl = "/images/demo-bicycles/kid-rent2.jpg",
                    IsAvailable = true,
                    Quantity = 5
                }
            );
        }
    }
}
