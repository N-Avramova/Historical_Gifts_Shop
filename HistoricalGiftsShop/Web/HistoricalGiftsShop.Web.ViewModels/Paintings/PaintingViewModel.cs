namespace HistoricalGiftsShop.Web.ViewModels.Paintings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HistoricalGiftsShop.Common;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class PaintingViewModel : IMapFrom<Painting>, IMapTo<Painting>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

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

        public string Code { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public PaintingType Paint { get; set; }

        public string CoverImageUrl
        {
            get
            {
                return this.ImageUrls != null && this.ImageUrls.Any() ? this.ImageUrls.Where(x => x.CoverImage == true).FirstOrDefault().ImageUrl : string.Empty;
            }
        }

        public IEnumerable<ImageViewModel> ImageUrls { get; set; }
    }
}
