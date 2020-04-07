namespace HistoricalGiftsShop.Services.Data
{
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;

    public interface IPaintingsService
    {
        Task<string> CreateAsync(string name, string description, string author, int categoryId, int stock, decimal price, string code, int length, int width, int height, PaintingType paint);

        T GetById<T>(string id);
    }
}
