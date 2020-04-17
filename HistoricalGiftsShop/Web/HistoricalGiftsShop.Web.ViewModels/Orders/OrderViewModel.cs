using HistoricalGiftsShop.Data.Models;
using HistoricalGiftsShop.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalGiftsShop.Web.ViewModels.Orders
{
    public class OrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string UserUserName { get; set; }

        public decimal OrderTotal { get; set; }

        public IEnumerable<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}
