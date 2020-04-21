namespace HistoricalGiftsShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public PaymentType PaymentType { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public decimal OrderTotal { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
