using BikeShop.Web.Models.Enum;

namespace BikeShop.Web.Models
{
    public class AccessoriesFilterViewModel
    {
        public AccessoriesCategory? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public List<AccessoriesCategory> AvailableCategories { get; set; } = new();
        public List<Accessories> Results { get; set; } = new();
    }
}
