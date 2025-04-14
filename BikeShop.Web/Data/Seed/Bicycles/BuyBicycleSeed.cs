using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Data.Seed.Bicycles
{
    public static class BuyBicycleSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bicycle>().HasData(
                new Bicycle
                {
                    Id = 1,
                    Name = "Drag C1 Road",
                    Description = "Лек и бърз шосеен велосипед, идеален за състезания и дълги карания по асфалт.",
                    Price = 3200,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.Road,
                    Brand = "DRAG",
                    FrameSize = "M",
                    ImageUrl = "/images/road1-1.jpg",
                    IsAvailable = true,
                    Quantity = 5
                },
                new Bicycle
                {
                    Id = 2,
                    Name = "Specialized Allez",
                    Description = "Надежден и ефективен шосеен велосипед с отлична аеродинамика.",
                    Price = 4600,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.Road,
                    Brand = "SPECIALIZED",
                    FrameSize = "L",
                    ImageUrl = "/images/road2-1.jpg",
                    IsAvailable = true,
                    Quantity = 3
                },
                new Bicycle
                {
                    Id = 3,
                    Name = "NS Bikes Zircus",
                    Description = "Маневрен хардтейл за трикове и каране в градска среда.",
                    Price = 2800,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.MountainHardtail,
                    Brand = "NS BIKES",
                    FrameSize = "S",
                    ImageUrl = "/images/hardtail1-1.jpg",
                    IsAvailable = true,
                    Quantity = 4
                },
                new Bicycle
                {
                    Id = 4,
                    Name = "Drag Hardy Pro",
                    Description = "Планински велосипед с твърда рамка, идеален за off-road каране.",
                    Price = 3000,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.MountainHardtail,
                    Brand = "DRAG",
                    FrameSize = "M",
                    ImageUrl = "/images/hardtail2-1.jpg",
                    IsAvailable = true,
                    Quantity = 7
                },
                new Bicycle
                {
                    Id = 5,
                    Name = "YT Jeffsy Core",
                    Description = "Сериозен планински велосипед с пълно окачване за максимален контрол по пресечени терени.",
                    Price = 5200,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.MountainFullSuspension,
                    Brand = "YT INDUSTRIES",
                    FrameSize = "L",
                    ImageUrl = "/images/full1-1.jpg",
                    IsAvailable = true,
                    Quantity = 6
                },
                new Bicycle
                {
                    Id = 6,
                    Name = "Specialized Stumpjumper Evo",
                    Description = "Премиум MTB за тежки терени. Със здрава рамка и модерна геометрия.",
                    Price = 5800,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.MountainFullSuspension,
                    Brand = "SPECIALIZED",
                    FrameSize = "XL",
                    ImageUrl = "/images/full2-1.jpg",
                    IsAvailable = true,
                    Quantity = 2
                },
                new Bicycle
                {
                    Id = 7,
                    Name = "Drag City Rider",
                    Description = "Удобен и стилен градски велосипед – идеален за ежедневно придвижване.",
                    Price = 2200,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.City,
                    Brand = "DRAG",
                    FrameSize = "M",
                    ImageUrl = "/images/city1-1.jpg",
                    IsAvailable = true,
                    Quantity = 8
                },
                new Bicycle
                {
                    Id = 8,
                    Name = "NS Bikes Metropolis",
                    Description = "Хибриден велосипед за град и офроуд. Съчетава комфорт и здравина.",
                    Price = 2500,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.City,
                    Brand = "NS BIKES",
                    FrameSize = "L",
                    ImageUrl = "/images/city2-1.jpg",
                    IsAvailable = true,
                    Quantity = 4
                },
                new Bicycle
                {
                    Id = 9,
                    Name = "Drag Kid 20\"",
                    Description = "Цветен велосипед за деца от 5 до 8 години. Сигурен и лек.",
                    Price = 950,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.Kids,
                    Brand = "DRAG",
                    FrameSize = "XS",
                    ImageUrl = "/images/kid1-1.jpg",
                    IsAvailable = true,
                    Quantity = 10
                },
                new Bicycle
                {
                    Id = 10,
                    Name = "YT Primus 24\"",
                    Description = "Качествен велосипед за деца от 8 до 12 г. Стабилен и удобен за каране.",
                    Price = 1050,
                    Type = BicycleType.ForSale,
                    Category = BicycleCategory.Kids,
                    Brand = "YT INDUSTRIES",
                    FrameSize = "S",
                    ImageUrl = "/images/kid2-1.jpg",
                    IsAvailable = true,
                    Quantity = 9
                }
            );
        }
    }
}
