namespace HistoricalGiftsShop.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HistoricalGiftsShop.Common;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class OrderCreateInputModel : IMapFrom<Order>, IMapFrom<ApplicationUser>, IMapTo<Order>
    {
        [Required(ErrorMessage = "Моля, въведете име.")]
        [StringLength(50)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Моля, въведете фамилия.")]
        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Моля, въведете имейл адрес.")]
        [Display(Name = "Имейл адрес")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Моля, въведете телефон за връзка.")]
        [Display(Name = "Телефон")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Моля, въведете адрес.")]
        [StringLength(100)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Моля, въведете пощенски код.")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Въведете пощенски код в следния формат: 1234")]
        [Display(Name = "Пощенски код")]
        [StringLength(4)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Моля, въведете държава.")]
        [Display(Name = "Държава")]
        [StringLength(50)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Моля, въведете град.")]
        [Display(Name = "Град")]
        [StringLength(50)]
        public string City { get; set; }

        [Display(Name = "Начин на плащане:")]
        [Required(ErrorMessage = "Изберете метод на плащане")]
        [BindProperty]
        public PaymentType PaymentType { get; set; }

        public decimal OrderTotal { get; set; }

        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public IEnumerable<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}
