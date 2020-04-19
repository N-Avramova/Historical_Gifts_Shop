namespace HistoricalGiftsShop.Web.ViewModels.Administration.Paintings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Web.ViewModels.Paintings;

    public class PaintingIndexViewModel
    {
        public IEnumerable<PaintingViewModel> Paintings { get; set; }
    }
}
