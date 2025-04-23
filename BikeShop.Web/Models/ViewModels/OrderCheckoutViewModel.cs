using System.ComponentModel.DataAnnotations;
using BikeShop.Web.Models;

public class OrderCheckoutViewModel : IValidatableObject
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

    public string? DeliveryCity { get; set; }
    public string? DeliveryStreet { get; set; }
    public string? DeliveryStreetNumber { get; set; }
    public string? PostalCode { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (IsDelivery)
        {
            if (string.IsNullOrWhiteSpace(DeliveryCity))
                yield return new ValidationResult("Моля, въведете град.", new[] { nameof(DeliveryCity) });

            if (string.IsNullOrWhiteSpace(DeliveryStreet))
                yield return new ValidationResult("Моля, въведете улица.", new[] { nameof(DeliveryStreet) });

            if (string.IsNullOrWhiteSpace(DeliveryStreetNumber))
                yield return new ValidationResult("Моля, въведете номер на улицата.", new[] { nameof(DeliveryStreetNumber) });

            if (string.IsNullOrWhiteSpace(PostalCode))
                yield return new ValidationResult("Моля, въведете пощенски код.", new[] { nameof(PostalCode) });
        }
    }
}
