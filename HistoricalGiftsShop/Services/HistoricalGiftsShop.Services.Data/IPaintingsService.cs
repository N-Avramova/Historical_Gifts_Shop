namespace HistoricalGiftsShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;

    public interface IPaintingsService
    {
        Task<string> CreateAsync(string name, string description, string author, int categoryId, int stock, decimal price, string code, int length, int width, int height, PaintingType paint);

        T GetById<T>(string id);

        Task UpdateStockAsync(string id, int quantity);

        IEnumerable<T> GetAll<T>();

        Task DeleteByIdAsync(string id);

        Task EditAsync(string id, string name, string description, string author, int categoryId, int stock, decimal price, string code, int length, int width, int height, PaintingType paint);

        IEnumerable<T> GetPaintingsByPage<T>(int? take = null, int skip = 0);

        int GetPaintingsCount();
    }
}
