namespace HistoricalGiftsShop.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Paintings;

    public class ProductInCategoryViewModel : IMapFrom<Book>, IMapFrom<Painting>, IMapFrom<Ceramic>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string TitleValue
        {
            get
            {
                return this.Name ??= this.Title;
            }
        }

        public decimal Price { get; set; }

        public string ProductImageUrl
        {
            get
            {
                return this.ImageUrls != null && this.ImageUrls.Any() ? this.ImageUrls.Where(x => x.CoverImage == true).FirstOrDefault().ImageUrl : this.ImageUrl;
            }
        }

        public string ImageUrl { get; set; }

        public IEnumerable<ImageViewModel> ImageUrls { get; set; }

        public int Stock { get; set; }

        public bool InStock
        {
            get
            {
                return this.Stock > 0;
            }
        }

        public string AddButtonText
        {
            get
            {
                return this.Stock > 0 ? this.Stock < 3 ? "Ограничена наличност" : "Добави в кошницата" : "Изчерпана наличност";
            }
        }
    }
}
