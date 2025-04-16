using System.ComponentModel.DataAnnotations;
using BikeShop.Web.Models;

public class RentalCheckoutViewModel
{
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();

    [Required(ErrorMessage = "Моля, въведете име.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Моля, въведете фамилия.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Моля, въведете телефонен номер.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Моля, въведете град.")]
    public string City { get; set; }

    [Required(ErrorMessage = "Изберете начална дата.")]
    [DataType(DataType.Date)]
    public DateTime RentalStartDate { get; set; }

    [Required(ErrorMessage = "Изберете крайна дата.")]
    [DataType(DataType.Date)]
    public DateTime RentalEndDate { get; set; }
}
