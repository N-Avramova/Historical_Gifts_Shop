namespace HistoricalGiftsShop.Web.ViewModels.Books
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Common;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class BookViewModel : IMapFrom<Book>, IMapTo<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public DateTime YearOfPublisher { get; set; }

        public Category Category { get; set; }

        public string Url => $"/c/{this.Category.Name.Replace(' ', '-')}";

        public int Stock { get; set; }

        public bool InStock
        {
            get
            {
                return this.Stock > 0;
            }
        }

        public string AvailableStock
        {
            get
            {
                return this.Stock > 0 ? this.Stock < 3 ? GlobalConstants.LimitedStock : GlobalConstants.InStock : GlobalConstants.OutOfStock;
            }
        }

        public decimal Price { get; set; }

        public BookCoverType CoverType { get; set; }

        public int? Pages { get; set; }

        public string Language { get; set; }

        public string ISBN { get; set; }

        public string ImageUrl { get; set; }
    }
}
