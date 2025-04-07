using System.ComponentModel.DataAnnotations;

namespace BikeShop.Web.Models
{
    

    public class GearItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public GearCategory Category { get; set; }
    }

}
