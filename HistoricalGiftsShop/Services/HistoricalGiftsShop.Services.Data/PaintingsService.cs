namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class PaintingsService : IPaintingsService
    {
        public readonly IDeletableEntityRepository<Painting> paintingsRepository;

        public PaintingsService(IDeletableEntityRepository<Painting> paintingsRepository)
        {
            this.paintingsRepository = paintingsRepository;
        }

        public async Task<string> CreateAsync(string name, string description, int categoryId, int stock, decimal price, string code, int length, int width, int height, PaintingType paint)
        {
            var painting = new Painting
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                Stock = stock,
                Price = price,
                Code = code,
                Length = length,
                Width = width,
                Height = height,
                Paint = paint,
            };

            await this.paintingsRepository.AddAsync(painting);
            await this.paintingsRepository.SaveChangesAsync();
            return painting.Id;
        }

        public T GetById<T>(string id)
        {
            var painting = this.paintingsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return painting;
        }
    }
}
