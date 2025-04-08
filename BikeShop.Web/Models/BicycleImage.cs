using BikeShop.Web.Models;
using System.ComponentModel.DataAnnotations;

public class BicycleImage
{
    public int Id { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    // Връзка с Bicycle
    public int BicycleId { get; set; }
    public Bicycle Bicycle { get; set; }
}
