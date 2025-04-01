// ✅ Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace BikeShop.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Тук можеш да добавиш FirstName, LastName и други свойства по желание
        // public string FirstName { get; set; }
        // public string LastName { get; set; }
    }
}
