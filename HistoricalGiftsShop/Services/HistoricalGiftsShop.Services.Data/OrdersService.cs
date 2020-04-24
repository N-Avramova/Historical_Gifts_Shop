namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HistoricalGiftsShop.Common.Enums;
    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<OrderDetails> orderDetailsRepository;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository, IDeletableEntityRepository<OrderDetails> orderDetailsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.orderDetailsRepository = orderDetailsRepository;
        }

        public async Task<int> CreateOrderAsync(string firstName, string lastName, string email, string phone, string address, string country, string city, string userId, PaymentType paymentType, decimal orderTotal)
        {
            var order = new Order
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Address = address,
                Country = country,
                City = city,
                UserId = userId,
                PaymentType = paymentType,
                OrderTotal = orderTotal,
                StatusType = OrderStatusType.Created,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }

        public async Task CreateOrderDetailsAsync(int orderId, Book book, Painting painting, int amount, decimal price)
        {
            var orderDetails = new OrderDetails
            {
                OrderId = orderId,
                Book = book,
                Painting = painting,
                Amount = amount,
                Price = price,
            };

            await this.orderDetailsRepository.AddAsync(orderDetails);
            await this.orderDetailsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetOrdersByUserId<T>(string userId)
        {
            return this.ordersRepository.All().Where(x => x.UserId == userId).To<T>().ToList();
        }

        public IEnumerable<T> GetOrderDetailsByOrderId<T>(int orderId)
        {
            return this.orderDetailsRepository.All().Include(b => b.Book).Include(p => p.Painting).Where(x => x.OrderId == orderId).To<T>().ToList();
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatusType statusType)
        {
            var order = this.ordersRepository.All().Where(x => x.Id == orderId).FirstOrDefault();
            order.StatusType = statusType;
            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync();
        }
    }
}
