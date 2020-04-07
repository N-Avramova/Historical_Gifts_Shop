namespace HistoricalGiftsShop.Data.Models
{
    using HistoricalGiftsShop.Data.Common.Models;

    public class Images : BaseDeletableModel<int>
    {
        public string PaintingId { get; set; }

        public virtual Painting Paintings { get; set; }

        public bool CoverImage { get; set; }

        public string ImageUrl { get; set; }
    }
}
