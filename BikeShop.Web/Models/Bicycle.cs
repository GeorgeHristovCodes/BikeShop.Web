using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShop.Web.Models
{

    public class Bicycle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        public BicycleType Type { get; set; }
        public BicycleCategory Category { get; set; }


        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int Quantity { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }



    }
}
