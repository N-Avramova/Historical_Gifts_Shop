namespace HistoricalGiftsShop.Web.ViewModels.Administration.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Books;
    using HistoricalGiftsShop.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class BookEditModel : IMapTo<Book>, IMapFrom<Book>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Издател")]
        public string Publisher { get; set; }

        public DateTime YearOfPublisher
        {
            get
            {
                return new DateTime(this.Year, 1, 1);
            }
        }

        [Required]
        [Display(Name = "Година на издаване")]
        public int Year { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string Url { get; set; }

        public CategoryViewModel Category { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Моля, въведете цяло число.")]
        [Range(0, int.MaxValue)]
        [Display(Name = "Наличност")]
        public int Stock { get; set; }

        [Required]
        [RegularExpression(@"^(0|[1-9]\d*)$", ErrorMessage = "Моля, въведете цената в следния вид: 12")]
        [Display(Name = "Цена (лв)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###}")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Вид корица")]
        public int BookCoverTypeId { get; set; }

        public IEnumerable<BookCoverTypeDropDownViewModel> BookCoverTypes { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Моля, въведете цяло число.")]
        [Range(0, int.MaxValue)]
        [Display(Name = "Брой страници")]
        public int? Pages { get; set; }

        [Required]
        [Display(Name = "Език")]
        public string Language { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}-[0-9]{10}", ErrorMessage = "Моля, въведете ISBN код в следният формат 123-1234567891")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "За да промените снимката на корицата, моля изберете нова снимка.")]
        public IFormFile ChangedImageUrl { get; set; }

        [Display(Name = "Снимка на корицата")]
        public string ImageUrl { get; set; }
    }
}
