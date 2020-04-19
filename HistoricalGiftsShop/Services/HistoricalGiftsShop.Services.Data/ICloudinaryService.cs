namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<List<string>> UploadAsync(ICollection<IFormFile> files);

        Task DeleteImageAsync(string imageUrl);
    }
}
