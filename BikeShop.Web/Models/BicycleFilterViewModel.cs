using BikeShop.Web.Models;

namespace BikeShop.Web.Models.ViewModels
{
    public class BicycleFilterViewModel
    {
        // 🔎 Филтри
        public BicycleCategory? Category { get; set; }
        public string? Brand { get; set; }
        public string? FrameSize { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        // 📦 Резултати
        public List<Bicycle> Results { get; set; } = new();

        // 🧾 За попълване на падащи менюта
        public List<string> AvailableBrands { get; set; } = new() { "DRAG", "NS BIKES", "SPECIALIZED", "YT INDUSTRIES" };
        public List<string> FrameSizes { get; set; } = new() { "XS", "S", "M", "L", "XL", "XXL" };
    }
}
