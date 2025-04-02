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

        [Required]
        public string Address { get; set; } = null!;


        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; }
    }
}
