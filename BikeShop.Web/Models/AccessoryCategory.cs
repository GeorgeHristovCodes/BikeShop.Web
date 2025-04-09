using System.ComponentModel.DataAnnotations;
namespace BikeShop.Web.Models
{


    public enum AccessoryCategory
    {
        [Display(Name = "Каска")]
        Helmet,

        [Display(Name = "Ръкавици")]
        Gloves,

        [Display(Name = "Помпа")]
        Pump,

        [Display(Name = "Инструменти")]
        Tools,

        [Display(Name = "Наколенки")]
        KneePads,

        [Display(Name = "Лакътници")]
        ElbowPads,

        [Display(Name = "Очила")]
        Glasses
    }
}

