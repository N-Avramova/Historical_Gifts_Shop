namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IImagesService
    {
        Task CreateAsync(string paintingId, bool coverImage, string imageUrl);

        IEnumerable<T> GetByPaintingId<T>(string id);
    }
}
