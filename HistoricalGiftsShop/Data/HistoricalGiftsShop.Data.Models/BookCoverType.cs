namespace HistoricalGiftsShop.Data.Models
{
    using HistoricalGiftsShop.Data.Common.Models;

    public class BookCoverType : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public bool HavePages { get; set; }
    }
}
