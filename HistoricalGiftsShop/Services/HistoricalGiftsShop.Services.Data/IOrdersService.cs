namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;

    public interface IOrdersService
    {
        Task<int> CreateOrderAsync(string firstName, string lastName, string email, string phone, string address, string country, string city, string userID, PaymentType payment, decimal orderTotal);

        Task CreateOrderDetailsAsync(int orderId, Book bookId, Painting paintingId, int amount, decimal price);

        // get order by current user
        IEnumerable<T> GetOrdersByUserId<T>(string userId);

        // get order details for current order
        IEnumerable<T> GetOrderDetailsByOrderId<T>(int orderId);
    }
}
