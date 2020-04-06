namespace HistoricalGiftsShop.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Url => $"/c/{this.Name.Replace(' ', '-')}";

        public IEnumerable<ProductInCategoryViewModel> Paintings { get; set; }

        public IEnumerable<ProductInCategoryViewModel> Books { get; set; }

        public IEnumerable<ProductInCategoryViewModel> Ceramics { get; set; }
    }
}
