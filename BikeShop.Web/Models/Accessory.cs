using BikeShop.Web.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Web.Models
{
    public class Accessories
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        public string Brand { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public AccessoriesCategory Category { get; set; }

        public int Stock { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
