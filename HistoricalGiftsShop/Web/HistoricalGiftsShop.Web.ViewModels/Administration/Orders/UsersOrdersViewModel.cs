namespace HistoricalGiftsShop.Web.ViewModels.Administration.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Web.ViewModels.Orders;

    public class UsersOrdersViewModel
    {
        public IEnumerable<UserViewModel> AllUsers { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }

        public string UserId { get; set; }
    }
}
