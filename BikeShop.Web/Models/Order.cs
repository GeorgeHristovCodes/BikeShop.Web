using BikeShop.Models;
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

        public int? BicycleId { get; set; }

        [ForeignKey("BicycleId")]
        public Bicycle? Bicycle { get; set; }

        public int? AccessoryId { get; set; }

        [ForeignKey("AccessoryId")]
        public Accessory? Accessory { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }

        public string? CustomerCity { get; set; }

        public string? DeliveryCity { get; set; }
        public string? DeliveryStreet { get; set; }
        public string? DeliveryStreetNumber { get; set; }
        public string? PostalCode { get; set; }

        public bool IsDelivery { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;




    }
}