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

        public async Task<string> CreateAsync(string name, string description, string author, int categoryId, int stock, decimal price, string code, int length, int width, int height, PaintingType paint)
        {
            var painting = new Painting
            {
                Name = name,
                Description = description,
                Author = author,
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

        public async Task DeleteByIdAsync(string id)
        {
            var painting = this.paintingsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.paintingsRepository.Delete(painting);
            await this.paintingsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(string id, string name, string description, string author, int categoryId, int stock, decimal price, string code, int length, int width, int height, PaintingType paint)
        {
            var painting = this.paintingsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (painting != null)
            {
                painting.Name = name;
                painting.Description = description;
                painting.Author = author;
                painting.CategoryId = categoryId;
                painting.Stock = stock;
                painting.Price = price;
                painting.Code = code;
                painting.Length = length;
                painting.Width = width;
                painting.Height = height;
                painting.Paint = paint;

                this.paintingsRepository.Update(painting);
                await this.paintingsRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.paintingsRepository.All().To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var painting = this.paintingsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return painting;
        }

        public IEnumerable<T> GetPaintingsByPage<T>(int? take = null, int skip = 0)
        {
            var query = this.paintingsRepository.All()
                .OrderByDescending(x => x.CreatedOn).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetPaintingsBySearchString<T>(string searchString)
        {
            return this.paintingsRepository.All().Where(x => x.Name.Contains(searchString)).To<T>().ToList();
        }

        public int GetPaintingsCount()
        {
            return this.paintingsRepository.All().Count();
        }

        public async Task UpdateStockAsync(string id, int quantity)
        {
            var painting = this.paintingsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            painting.Stock = painting.Stock - quantity;
            this.paintingsRepository.Update(painting);
            await this.paintingsRepository.SaveChangesAsync();
        }
    }
}
