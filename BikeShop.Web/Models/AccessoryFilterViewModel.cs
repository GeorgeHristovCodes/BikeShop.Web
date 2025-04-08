namespace BikeShop.Web.Models
{
    public class AccessoryFilterViewModel
    {
        public AccessoryCategory? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public List<AccessoryCategory> AvailableCategories { get; set; } = new();
        public List<Accessory> Results { get; set; } = new();
    }
}
