namespace BikeShop.Web.Models
{
    public class HomeViewModel
    {
        public List<Bicycle> BicyclesForSale { get; set; } = new();
        public List<Bicycle> BicyclesForRent { get; set; } = new();
    }
}
