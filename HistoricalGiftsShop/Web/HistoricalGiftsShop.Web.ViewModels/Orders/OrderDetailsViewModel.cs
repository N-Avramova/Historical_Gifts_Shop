namespace HistoricalGiftsShop.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class OrderDetailsViewModel : IMapFrom<OrderDetails>, IMapFrom<ShoppingCartItem>, IMapTo<OrderDetails>, IHaveCustomMappings
    {
        public int OrderId { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public string PaintingId { get; set; }

        public Painting Painting { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string NameByProduct
        {
            get
            {
                return this.Painting != null ? this.Painting.Name : this.Book.Title;
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ShoppingCartItem, OrderDetailsViewModel>()
                .ForMember(
                    m => m.Price,
                    opt => opt.MapFrom(x => x.Painting != null ? x.Painting.Price : x.Book.Price))
                .ForMember(
                    m => m.ProductName,
                    opt => opt.MapFrom(x => x.Painting != null ? x.Painting.Name : x.Book.Title))
                .ForMember(
                    m => m.ProductId,
                    opt => opt.MapFrom(x => x.Painting != null ? x.Painting.Id : x.Book.Id.ToString()));
        }
    }
}
