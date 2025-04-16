using System.ComponentModel.DataAnnotations;
using BikeShop.Web.Models;

public class OrderCheckoutViewModel
{
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();

    // Данни за клиента
    [Required(ErrorMessage = "Моля, въведете име.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Моля, въведете фамилия.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Моля, въведете телефонен номер.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Телефонният номер трябва да съдържа точно 10 цифри.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Моля, въведете град.")]
    public string? CustomerCity { get; set; }

    // Адрес за доставка
    public bool IsDelivery { get; set; }

    [Required(ErrorMessage = "Моля, въведете град.")]
    public string? DeliveryCity { get; set; }
    [Required(ErrorMessage = "Моля, въведете улица.")]
    public string? DeliveryStreet { get; set; }
    [Required(ErrorMessage = "Моля, въведете номер на улицата.")]
    public string? DeliveryStreetNumber { get; set; }

    [Required(ErrorMessage = "Моля, въведете пощенски код.")]
    public string? PostalCode { get; set; }
}
