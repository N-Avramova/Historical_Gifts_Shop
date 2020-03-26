namespace HistoricalGiftsShop.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;

    internal class BookCoverTypesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BookCoverTypes.Any())
            {
                return;
            }

            List<BookCoverType> bookCoverTypes = new List<BookCoverType>();
            bookCoverTypes.Add(new BookCoverType
            {
                Name = "Paperback",
                Value = "мека",
                HavePages = true,
            });
            bookCoverTypes.Add(new BookCoverType
            {
                Name = "Hardback",
                Value = "твърда",
                HavePages = true,
            });
            bookCoverTypes.Add(new BookCoverType
            {
                Name = "Audio",
                Value = "CD-Audio",
                HavePages = false,
            });
            bookCoverTypes.Add(new BookCoverType
            {
                Name = "Digital",
                Value = "DVD video",
                HavePages = false,
            });

            await dbContext.BookCoverTypes.AddRangeAsync(bookCoverTypes);
        }
    }
}
