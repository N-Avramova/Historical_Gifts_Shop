namespace HistoricalGiftsShop.Web.ViewModels.ShoppingCarts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Books;
    using HistoricalGiftsShop.Web.ViewModels.Paintings;

    public class ShoppingCartItemViewModel : IMapFrom<ShoppingCartItem>
    {
        public Painting Painting { get; set; }

        public Book Book { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }

        public string ProductControllerName
        {
            get
            {
                return this.Painting != null ? "Paintings" : "Books";
            }
        }

        public decimal ProductPrice
        {
            get
            {
                return this.Painting != null ? this.Painting.Price : this.Book.Price;
            }
        }

        public string ProductId
        {
            get
            {
                return this.Painting != null ? this.Painting.Id : this.Book.Id.ToString();
            }
        }

        public string ProductName
        {
            get
            {
                return this.Painting != null ? this.Painting.Name : this.Book.Title;
            }
        }
    }
}
