namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICeramicsService
    {
        Task<int> CreateAsync(string name, string description, int categoryId, int stock, decimal price, string code, int capacity, string measure);

        T GetById<T>(int id);
    }
}
