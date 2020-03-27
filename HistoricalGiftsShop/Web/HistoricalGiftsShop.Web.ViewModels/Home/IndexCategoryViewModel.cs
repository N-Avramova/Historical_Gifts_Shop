namespace HistoricalGiftsShop.Web.ViewModels.Home
{
    using AutoMapper;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Url => $"/c/{this.Name.Replace(' ', '-')}";
    }
}
