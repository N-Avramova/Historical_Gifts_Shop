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

    public class PaintingsServiceTests
    {
        [Fact]
        public void TestGetPaintingCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Painting>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Painting { Name = "test" }).GetAwaiter().GetResult();
            repository.AddAsync(new Painting { Name = "test_one" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var paintingService = new PaintingsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPainiting).Assembly);
            var count = paintingService.GetPaintingsCount();

            Assert.Equal(2, count);
        }

        public class MyTestPainiting : IMapFrom<Painting>
        {
            public string Title { get; set; }
        }
    }
}
