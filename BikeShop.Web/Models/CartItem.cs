using BikeShop.Web.Models;
using BikeShop.Web.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    [Key]
    public int Id { get; set; }

    // Велосипед (по избор)
    public int? BicycleId { get; set; }

    [ForeignKey(nameof(BicycleId))]
    public Bicycle? Bicycle { get; set; }

    //  Добавяме Аксесоар (по избор)
    public int? AccessoriesId { get; set; }

    [ForeignKey(nameof(AccessoriesId))]
    public Accessories? Accessories { get; set; }

    // Връзка с потребителя
    [Required]
    public string UserId { get; set; } = null!;

    // Тип: Покупка или Наем
    [Required]
    public CartItemType Type { get; set; }

    // Само ако е за наем
    public DateTime? RentalStartDate { get; set; }
    public DateTime? RentalEndDate { get; set; }

    [Required]
    [Range(0, 100000)]
    public decimal Price { get; set; }

    // Брой (за аксесоари)
    public int Quantity { get; set; } = 1;
}
