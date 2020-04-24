namespace HistoricalGiftsShop.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using HistoricalGiftsShop.Common.Enums;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>, IHaveCustomMappings
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

        public PaymentType PaymentType { get; set; }

        [Display(Name = "Статус на поръчката")]
        public OrderStatusType StatusType { get; set; }

        public int StatusTypeInt { get; set; }

        public string UserUserName { get; set; }

        public decimal OrderTotal { get; set; }

        public IEnumerable<OrderDetailsViewModel> OrderDetails { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderViewModel>()
                .ForMember(
                    m => m.StatusTypeInt,
                    opt => opt.MapFrom(x => (int)x.StatusType));
        }
    }
}
