namespace HistoricalGiftsShop.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Data;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Data.Repositories;
    using HistoricalGiftsShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class BooksServiceTests
    {
        [Fact]
        public void TestGetBookById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Book>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Book { Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var bookService = new BooksService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestBook).Assembly);
            var book = bookService.GetById<MyTestBook>(1);

            Assert.Equal("test", book.Title);
        }

        public class MyTestBook : IMapFrom<Book>
        {
            public string Title { get; set; }
        }
    }
}
