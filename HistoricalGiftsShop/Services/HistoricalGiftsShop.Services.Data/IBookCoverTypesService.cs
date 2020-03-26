namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBookCoverTypesService
    {
        // for future use
        // Task CreateAsync(string name, string value, bool havePages);
        IEnumerable<T> GetAll<T>();
    }
}
