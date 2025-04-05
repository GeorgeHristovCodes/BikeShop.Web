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
    public string PhoneNumber { get; set; }

    public string? CustomerCity { get; set; }

    // Адрес за доставка
    public bool IsDelivery { get; set; }

    public string? DeliveryCity { get; set; }
    public string? DeliveryStreet { get; set; }
    public string? DeliveryStreetNumber { get; set; }
    public string? PostalCode { get; set; }
}
