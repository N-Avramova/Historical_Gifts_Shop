namespace HistoricalGiftsShop.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class AllCategoriesViewModel
    {
        public string SearchString { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
