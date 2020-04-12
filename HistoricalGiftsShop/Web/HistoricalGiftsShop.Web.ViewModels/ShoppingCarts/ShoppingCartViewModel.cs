namespace HistoricalGiftsShop.Web.ViewModels.ShoppingCarts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        public decimal ShoppingCartTotal { get; set; }

        public string ReturnUrl { get; set; }
    }
}
