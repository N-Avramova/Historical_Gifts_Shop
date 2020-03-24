namespace HistoricalGiftsShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class BookCoverTypesService : IBookCoverTypesService
    {
        private readonly IDeletableEntityRepository<BookCoverType> bookCoverTypesRepository;

        public BookCoverTypesService(IDeletableEntityRepository<BookCoverType> bookCoverTypesRepository)
        {
            this.bookCoverTypesRepository = bookCoverTypesRepository;
        }

        public async Task CreateAsync(string name, string value, bool havePages)
        {
            var bookCoverType = new BookCoverType
            {
                Name = name,
                Value = value,
                HavePages = havePages,
            };

            await this.bookCoverTypesRepository.AddAsync(bookCoverType);
            await this.bookCoverTypesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.bookCoverTypesRepository.All().To<T>().ToList();
        }
    }
}
