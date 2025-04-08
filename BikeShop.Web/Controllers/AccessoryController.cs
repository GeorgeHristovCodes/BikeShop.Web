using Microsoft.AspNetCore.Mvc;

namespace BikeShop.Web.Controllers
{
    public class AccessoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
