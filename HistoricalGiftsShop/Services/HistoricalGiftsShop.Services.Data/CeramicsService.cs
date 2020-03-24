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

    public class CeramicsService : ICeramicsService
    {
        private readonly IDeletableEntityRepository<Ceramic> ceramicsRepository;

        public CeramicsService(IDeletableEntityRepository<Ceramic> ceramicsRepository)
        {
            this.ceramicsRepository = ceramicsRepository;
        }

        public async Task<int> CreateAsync(string name, string description, int categoryId, int stock, decimal price, string code, int capacity, string measure)
        {
            var ceramic = new Ceramic
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                Stock = stock,
                Price = price,
                Code = code,
                Capacity = capacity,
                Measure = measure,
            };

            await this.ceramicsRepository.AddAsync(ceramic);
            await this.ceramicsRepository.SaveChangesAsync();
            return ceramic.Id;
        }

        public T GetById<T>(int id)
        {
            var ceramic = this.ceramicsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return ceramic;
        }
    }
}
