namespace HistoricalGiftsShop.Web.ViewModels.Administration.Paintings
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Categories;
    using HistoricalGiftsShop.Web.ViewModels.Paintings;
    using Microsoft.AspNetCore.Http;

    public class PaintingEditModel : IMapTo<Painting>, IMapFrom<Painting>
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Наименование на картината")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string Url { get; set; }

        public CategoryViewModel Category { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Моля, въведете цяло число.")]
        [Range(0, int.MaxValue)]
        [Display(Name = "Наличност")]
        public int Stock { get; set; }

        [Required]
        [RegularExpression(@"^(0|[1-9]\d*)(\.\d+)?$", ErrorMessage = "Моля, въведете цената в един от следните формати: 15 или 15.35")]
        [Display(Name = "Цена (лв)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###.###}")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Код на продукта")]
        public string Code { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Моля, въведете цяло число.")]
        [Range(0, int.MaxValue)]
        [Display(Name = "Дължина на картината (cm)")]
        public int Length { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Моля, въведете цяло число.")]
        [Range(0, int.MaxValue)]
        [Display(Name = "Ширина на картината (cm)")]
        public int Width { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Моля, въведете цяло число.")]
        [Range(0, int.MaxValue)]
        [Display(Name = "Височина на картината (cm)")]
        public int Height { get; set; }

        [Required]
        [EnumDataType(typeof(PaintingType))]
        [Display(Name = "Използван тип боя")]
        public PaintingType Paint { get; set; }

        public IFormFile CoverImageUrl { get; set; }

        [Display(Name = "Допълнителни снимки")]
        public IEnumerable<IFormFile> AdditionalImageUrls { get; set; }

        public IEnumerable<ImageViewModel> ImageUrls { get; set; }

        public string ImageUrlForDelete { get; set; }

        public string ActionType { get; set; }
    }
}
