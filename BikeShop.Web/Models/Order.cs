using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Web.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        [Required]
        public int BicycleId { get; set; }

        [ForeignKey("BicycleId")]
        public Bicycle? Bicycle { get; set; }

        


        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        // Информация за клиента
        public string? CustomerCity { get; set; }

        public string? DeliveryCity { get; set; }
        public string? DeliveryStreet { get; set; }
        public string? DeliveryStreetNumber { get; set; }
        public string? PostalCode { get; set; }

        // Начин на доставка
        public bool IsDelivery { get; set; } // true = доставка, false = взима от магазин
        public decimal TotalPrice { get; set; }
    }
}
