using System.ComponentModel.DataAnnotations;

namespace BikeShop.Web.Models
{
    public class Bicycle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        public BicycleType Type { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
