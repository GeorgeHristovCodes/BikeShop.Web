using BikeShop.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Data.Seed.Bicycles
{
    public static class BicycleImageSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BicycleImage>().HasData(
                // Bicycle 1
                new BicycleImage { Id = 1, BicycleId = 1, ImageUrl = "/images/demo-bicycles/road1-1.jpg" },
                new BicycleImage { Id = 2, BicycleId = 1, ImageUrl = "/images/demo-bicycles/road1-2.jpg" },
                new BicycleImage { Id = 3, BicycleId = 1, ImageUrl = "/images/demo-bicycles/road1-3.jpg" },

                // Bicycle 2
                new BicycleImage { Id = 4, BicycleId = 2, ImageUrl = "/images/demo-bicycles/road2-1.jpg" },
                new BicycleImage { Id = 5, BicycleId = 2, ImageUrl = "/images/demo-bicycles/road2-2.jpg" },
                new BicycleImage { Id = 6, BicycleId = 2, ImageUrl = "/images/demo-bicycles/road2-3.jpg" },

                // Bicycle 3
                new BicycleImage { Id = 7, BicycleId = 3, ImageUrl = "/images/demo-bicycles/hardtail1-1.jpg" },
                new BicycleImage { Id = 8, BicycleId = 3, ImageUrl = "/images/demo-bicycles/hardtail1-2.jpg" },
                new BicycleImage { Id = 9, BicycleId = 3, ImageUrl = "/images/demo-bicycles/hardtail1-3.jpg" },

                // Bicycle 4
                new BicycleImage { Id = 10, BicycleId = 4, ImageUrl = "/images/demo-bicycles/hardtail2-1.jpg" },
                new BicycleImage { Id = 11, BicycleId = 4, ImageUrl = "/images/demo-bicycles/hardtail2-2.jpg" },
                new BicycleImage { Id = 12, BicycleId = 4, ImageUrl = "/images/demo-bicycles/hardtail2-3.jpg" },

                // Bicycle 5
                new BicycleImage { Id = 13, BicycleId = 5, ImageUrl = "/images/demo-bicycles/full1-1.jpg" },
                new BicycleImage { Id = 14, BicycleId = 5, ImageUrl = "/images/demo-bicycles/full1-2.jpg" },
                new BicycleImage { Id = 15, BicycleId = 5, ImageUrl = "/images/demo-bicycles/full1-3.jpg" },

                // Bicycle 6
                new BicycleImage { Id = 16, BicycleId = 6, ImageUrl = "/images/demo-bicycles/full2-1.jpg" },
                new BicycleImage { Id = 17, BicycleId = 6, ImageUrl = "/images/demo-bicycles/full2-2.jpg" },
                new BicycleImage { Id = 18, BicycleId = 6, ImageUrl = "/images/demo-bicycles/full2-3.jpg" },

                // Bicycle 7
                new BicycleImage { Id = 19, BicycleId = 7, ImageUrl = "/images/demo-bicycles/city1-1.jpg" },
                new BicycleImage { Id = 20, BicycleId = 7, ImageUrl = "/images/demo-bicycles/city1-2.jpg" },
                new BicycleImage { Id = 21, BicycleId = 7, ImageUrl = "/images/demo-bicycles/city1-3.jpg" },

                // Bicycle 8
                new BicycleImage { Id = 22, BicycleId = 8, ImageUrl = "/images/demo-bicycles/city2-1.jpg" },
                new BicycleImage { Id = 23, BicycleId = 8, ImageUrl = "/images/demo-bicycles/city2-2.jpg" },
                new BicycleImage { Id = 24, BicycleId = 8, ImageUrl = "/images/demo-bicycles/city2-3.jpg" },

                // Bicycle 9
                new BicycleImage { Id = 25, BicycleId = 9, ImageUrl = "/images/demo-bicycles/kid1-1.jpg" },
                new BicycleImage { Id = 26, BicycleId = 9, ImageUrl = "/images/demo-bicycles/kid1-2.jpg" },
                new BicycleImage { Id = 27, BicycleId = 9, ImageUrl = "/images/demo-bicycles/kid1-3.jpg" },

                // Bicycle 10
                new BicycleImage { Id = 28, BicycleId = 10, ImageUrl = "/images/demo-bicycles/kid2-1.jpg" },
                new BicycleImage { Id = 29, BicycleId = 10, ImageUrl = "/images/demo-bicycles/kid2-2.jpg" },
                new BicycleImage { Id = 30, BicycleId = 10, ImageUrl = "/images/demo-bicycles/kid2-3.jpg" },





                //ЗА НАЕМ СНИМКИ НА ВЕЛОСИПЕДИ

                 // Bicycle 11
                new BicycleImage { Id = 31, BicycleId = 11, ImageUrl = "/images/demo-bicycles/road-rent1.jpg" },
                new BicycleImage { Id = 32, BicycleId = 11, ImageUrl = "/images/demo-bicycles/road-rent1-2.jpg" },
                new BicycleImage { Id = 33, BicycleId = 11, ImageUrl = "/images/demo-bicycles/road-rent1-3.jpg" },

                // Bicycle 12
                new BicycleImage { Id = 34, BicycleId = 12, ImageUrl = "/images/demo-bicycles/road-rent2.jpg" },
                new BicycleImage { Id = 35, BicycleId = 12, ImageUrl = "/images/demo-bicycles/road-rent2-2.jpg" },
                new BicycleImage { Id = 36, BicycleId = 12, ImageUrl = "/images/demo-bicycles/road-rent2-3.jpg" },

                // Bicycle 13
                new BicycleImage { Id = 37, BicycleId = 13, ImageUrl = "/images/demo-bicycles/hardtail-rent1.jpg" },
                new BicycleImage { Id = 38, BicycleId = 13, ImageUrl = "/images/demo-bicycles/hardtail-rent1-2.jpg" },
                new BicycleImage { Id = 39, BicycleId = 13, ImageUrl = "/images/demo-bicycles/hardtail-rent1-3.jpg" },

                // Bicycle 14
                new BicycleImage { Id = 40, BicycleId = 14, ImageUrl = "/images/demo-bicycles/hardtail-rent2.jpg" },
                new BicycleImage { Id = 41, BicycleId = 14, ImageUrl = "/images/demo-bicycles/hardtail-rent2-2.jpg" },
                new BicycleImage { Id = 42, BicycleId = 14, ImageUrl = "/images/demo-bicycles/hardtail-rent2-3.jpg" },

                // Bicycle 15
                new BicycleImage { Id = 43, BicycleId = 15, ImageUrl = "/images/demo-bicycles/full-rent1.jpg" },
                new BicycleImage { Id = 44, BicycleId = 15, ImageUrl = "/images/demo-bicycles/full-rent1-2.jpg" },
                new BicycleImage { Id = 45, BicycleId = 15, ImageUrl = "/images/demo-bicycles/full-rent1-3.jpg" },

                // Bicycle 16
                new BicycleImage { Id = 46, BicycleId = 16, ImageUrl = "/images/demo-bicycles/full-rent2.jpg" },
                new BicycleImage { Id = 47, BicycleId = 16, ImageUrl = "/images/demo-bicycles/full-rent2-2.jpg" },
                new BicycleImage { Id = 48, BicycleId = 16, ImageUrl = "/images/demo-bicycles/full-rent2-3.jpg" },

                // Bicycle 17
                new BicycleImage { Id = 49, BicycleId = 17, ImageUrl = "/images/demo-bicycles/city-rent1.jpg" },
                new BicycleImage { Id = 50, BicycleId = 17, ImageUrl = "/images/demo-bicycles/city-rent1-2.jpg" },
                new BicycleImage { Id = 51, BicycleId = 17, ImageUrl = "/images/demo-bicycles/city-rent1-3.jpg" },

                // Bicycle 18
                new BicycleImage { Id = 52, BicycleId = 18, ImageUrl = "/images/demo-bicycles/city-rent2.jpg" },
                new BicycleImage { Id = 53, BicycleId = 18, ImageUrl = "/images/demo-bicycles/city-rent2-2.jpg" },
                new BicycleImage { Id = 54, BicycleId = 18, ImageUrl = "/images/demo-bicycles/city-rent2-3.jpg" },

                // Bicycle 19
                new BicycleImage { Id = 55, BicycleId = 19, ImageUrl = "/images/demo-bicycles/kid-rent1.jpg" },
                new BicycleImage { Id = 56, BicycleId = 19, ImageUrl = "/images/demo-bicycles/kid-rent1-2.jpg" },
                new BicycleImage { Id = 57, BicycleId = 19, ImageUrl = "/images/demo-bicycles/kid-rent1-3.jpg" },

                // Bicycle 20
                new BicycleImage { Id = 58, BicycleId = 20, ImageUrl = "/images/demo-bicycles/kid-rent2.jpg" },
                new BicycleImage { Id = 59, BicycleId = 20, ImageUrl = "/images/demo-bicycles/kid-rent2-2.jpg" },
                new BicycleImage { Id = 60, BicycleId = 20, ImageUrl = "/images/demo-bicycles/kid-rent2-3.jpg" }
            );
        }
    }
}
