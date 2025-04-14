using System.ComponentModel.DataAnnotations;

namespace BikeShop.Web.Models.Enum
{


    public enum AccessoriesCategory
    {
        [Display(Name = "Каски")]
        Helmet,

        [Display(Name = "Ръкавици")]
        Gloves,

        [Display(Name = "Помпи")]
        Pump,

        [Display(Name = "Инструменти")]
        Tools,

        [Display(Name = "Наколенки")]
        KneePads,

        [Display(Name = "Налакътници")]
        ElbowPads,

        [Display(Name = "Очила")]
        Glasses
    }
}

