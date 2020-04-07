namespace HistoricalGiftsShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum PaintingType
    {
        [Display(Name = "Маслени бои")]
        Oil,
        [Display(Name = "Акрилни бои")]
        Acrylic,
        [Display(Name = "Темперни бои")]
        Tempering,
        [Display(Name = "Акварел")]
        Watercolor,
    }
}
