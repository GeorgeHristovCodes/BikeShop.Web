using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.Web.Data
{
    public static class AccessoriesSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessories>().HasData(

                // Каски
                new Accessories { Id = 1, Name = "Каска DRAG One", Brand = "DRAG", Category = AccessoriesCategory.Helmet, Price = 89, Stock = 12, ImageUrl = "/images/demo-accessories/helmet1.jpg", Description = "Лека и удобна каска за градски и планински маршрути." },
                new Accessories { Id = 2, Name = "Каска Specialized Pro", Brand = "SPECIALIZED", Category = AccessoriesCategory.Helmet, Price = 119, Stock = 8, ImageUrl = "/images/demo-accessories/helmet2.jpg", Description = "Професионална каска с вентилация и регулируема лента." },
                new Accessories { Id = 3, Name = "Каска CitySafe", Brand = "NS BIKES", Category = AccessoriesCategory.Helmet, Price = 65, Stock = 15, ImageUrl = "/images/demo-accessories/helmet3.jpg", Description = "Градска каска със стилен дизайн и защита." },

                // Ръкавици
                new Accessories { Id = 4, Name = "Ръкавици DRAG Short", Brand = "DRAG", Category = AccessoriesCategory.Gloves, Price = 35, Stock = 20, ImageUrl = "/images/demo-accessories/gloves1.jpg", Description = "Къси ръкавици за лятно каране с дишаща материя." },
                new Accessories { Id = 5, Name = "Ръкавици NS Grip", Brand = "NS BIKES", Category = AccessoriesCategory.Gloves, Price = 42, Stock = 14, ImageUrl = "/images/demo-accessories/gloves2.jpg", Description = "Гумени елементи за по-добро сцепление и комфорт." },
                new Accessories { Id = 6, Name = "Ръкавици UrbanRide", Brand = "SPECIALIZED", Category = AccessoriesCategory.Gloves, Price = 29, Stock = 18, ImageUrl = "/images/demo-accessories/gloves3.jpg", Description = "Ежедневни ръкавици за кратки разходки." },

                // Помпи
                new Accessories { Id = 7, Name = "Помпа DRAG Mini", Brand = "DRAG", Category = AccessoriesCategory.Pump, Price = 22, Stock = 25, ImageUrl = "/images/demo-accessories/pump1.jpg", Description = "Лека преносима помпа с универсален вентил." },
                new Accessories { Id = 8, Name = "Помпа Specialized Air", Brand = "SPECIALIZED", Category = AccessoriesCategory.Pump, Price = 45, Stock = 10, ImageUrl = "/images/demo-accessories/pump2.jpg", Description = "Помпа с манометър и алуминиево тяло." },
                new Accessories { Id = 9, Name = "Помпа TrailPump", Brand = "NS BIKES", Category = AccessoriesCategory.Pump, Price = 35, Stock = 18, ImageUrl = "/images/demo-accessories/pump3.jpg", Description = "Здрава помпа с удобна дръжка за планински велосипеди." },

                // Инструменти
                new Accessories { Id = 10, Name = "Инструментален комплект 9 в 1", Brand = "SPECIALIZED", Category = AccessoriesCategory.Tools, Price = 55, Stock = 11, ImageUrl = "/images/demo-accessories/tool1.jpg", Description = "Комплект с най-нужните инструменти за пътя." },
                new Accessories { Id = 11, Name = "Мултиинструмент NS", Brand = "NS BIKES", Category = AccessoriesCategory.Tools, Price = 49, Stock = 7, ImageUrl = "/images/demo-accessories/tool2.jpg", Description = "Компактен мултиинструмент за чанта или джоб." },
                new Accessories { Id = 12, Name = "Инструменти HomeFix", Brand = "DRAG", Category = AccessoriesCategory.Tools, Price = 60, Stock = 6, ImageUrl = "/images/demo-accessories/tool3.jpg", Description = "Инструментариум за домашни ремонти на велосипеда." },

                // Наколенки
                new Accessories { Id = 13, Name = "Наколенки DRAG Protect", Brand = "DRAG", Category = AccessoriesCategory.KneePads, Price = 75, Stock = 10, ImageUrl = "/images/demo-accessories/knee1.jpg", Description = "Подсилени наколенки за планинско каране." },
                new Accessories { Id = 14, Name = "Наколенки Trail Soft", Brand = "NS BIKES", Category = AccessoriesCategory.KneePads, Price = 59, Stock = 12, ImageUrl = "/images/demo-accessories/knee2.jpg", Description = "Меки и удобни наколенки с проветрив материал." },
                new Accessories { Id = 15, Name = "Наколенки CityFlex", Brand = "SPECIALIZED", Category = AccessoriesCategory.KneePads, Price = 49, Stock = 9, ImageUrl = "/images/demo-accessories/knee3.jpg", Description = "Градски стил с основна защита." },

                // Лакътници
                new Accessories { Id = 16, Name = "Лакътници DRAG Shield", Brand = "DRAG", Category = AccessoriesCategory.ElbowPads, Price = 55, Stock = 10, ImageUrl = "/images/demo-accessories/elbow1.jpg", Description = "Здрава защита за лакти при агресивно каране." },
                new Accessories { Id = 17, Name = "Лакътници UrbanPad", Brand = "SPECIALIZED", Category = AccessoriesCategory.ElbowPads, Price = 38, Stock = 12, ImageUrl = "/images/demo-accessories/elbow2.jpg", Description = "Защита с гъвкав дизайн и комфортна подложка." },
                new Accessories { Id = 18, Name = "Лакътници LightRide", Brand = "NS BIKES", Category = AccessoriesCategory.ElbowPads, Price = 29, Stock = 15, ImageUrl = "/images/demo-accessories/elbow3.jpg", Description = "Леки лакътници за ежедневно каране." },

                // Очила
                new Accessories { Id = 19, Name = "Очила DRAG ProVision", Brand = "DRAG", Category = AccessoriesCategory.Glasses, Price = 89, Stock = 13, ImageUrl = "/images/demo-accessories/glasses1.jpg", Description = "Слънцезащитни очила с UV филтър." },
                new Accessories { Id = 20, Name = "Очила TrailVision", Brand = "NS BIKES", Category = AccessoriesCategory.Glasses, Price = 95, Stock = 11, ImageUrl = "/images/demo-accessories/glasses2.jpg", Description = "Спортен дизайн и сменяеми стъкла." },
                new Accessories { Id = 21, Name = "Очила CityView", Brand = "SPECIALIZED", Category = AccessoriesCategory.Glasses, Price = 70, Stock = 16, ImageUrl = "/images/demo-accessories/glasses3.jpg", Description = "Очилата за модерни велосипедисти в града." }
            );
        }
    }
}
