namespace HistoricalGiftsShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Data.Common.Models;

    public class OrderDetails : BaseDeletableModel<int>
    {
        public int OrderId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Painting Painting { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }
    }
}
