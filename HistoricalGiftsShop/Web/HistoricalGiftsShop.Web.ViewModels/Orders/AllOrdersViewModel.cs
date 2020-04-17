namespace HistoricalGiftsShop.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllOrdersViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}
