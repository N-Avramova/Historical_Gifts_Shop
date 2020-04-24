namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBooksService
    {
        Task<int> CreateAsync(string title, string description, string author, string publisher, DateTime yearOfPublisher, int categoryId, int stock, decimal price, int bookCoverTypeId, int? pages, string language, string isbn, string imageUrl);

        T GetById<T>(int id);

        Task UpdateStockAsync(int id, int quantity);

        IEnumerable<T> GetAll<T>();

        Task DeleteByIdAsync(int id);

        Task EditAsync(int id, string title, string description, string author, string publisher, DateTime yearOfPublisher, int categoryId, int stock, decimal price, int bookCoverTypeId, int? pages, string language, string isbn, string imageUrl);

        IEnumerable<T> GetBooksByPage<T>(int? take = null, int skip = 0);

        int GetBooksCount();
    }
}
