namespace HistoricalGiftsShop.Web.ViewModels.Books
{
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class BookCoverTypeDropDownViewModel : IMapFrom<BookCoverType>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public bool HavePages { get; set; }
    }
}
