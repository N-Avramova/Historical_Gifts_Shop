namespace HistoricalGiftsShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using HistoricalGiftsShop.Data.Common.Models;

    public class ShoppingCartItem : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Painting Painting { get; set; }

        public virtual Book Book { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
