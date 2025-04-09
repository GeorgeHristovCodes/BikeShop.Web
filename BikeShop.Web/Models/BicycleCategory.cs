using System.ComponentModel.DataAnnotations;

public enum BicycleCategory
{
    [Display(Name = "Шосеен")]
    Road,

    [Display(Name = "Планински (твърда рамка)")]
    MountainHardtail,

    [Display(Name = "Планински (с окачване)")]
    MountainFullSuspension,

    [Display(Name = "Градски")]
    City,

    [Display(Name = "Детски")]
    Kids
}
