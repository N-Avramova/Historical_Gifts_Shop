namespace HistoricalGiftsShop.Web.ViewModels.Administration.Books
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Web.ViewModels.Books;

    public class BookIndexViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}
