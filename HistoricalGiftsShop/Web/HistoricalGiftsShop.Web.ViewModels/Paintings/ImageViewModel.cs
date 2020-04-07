namespace HistoricalGiftsShop.Web.ViewModels.Paintings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class ImageViewModel : IMapFrom<Images>
    {
        public bool CoverImage { get; set; }

        public string ImageUrl { get; set; }
    }
}
