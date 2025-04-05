using BikeShop.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BicycleId { get; set; }

    [ForeignKey(nameof(BicycleId))]
    public Bicycle Bicycle { get; set; }

    [Required]
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    [Required]
    public CartItemType Type { get; set; }

    public DateTime? RentalStartDate { get; set; }
    public DateTime? RentalEndDate { get; set; }

    [Required]
    [Range(0, 100000)]
    public decimal Price { get; set; }
}
