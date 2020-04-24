namespace HistoricalGiftsShop.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    using HistoricalGiftsShop.Data;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Data.Repositories;
    using HistoricalGiftsShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class OrdersServiceTests
    {
        [Fact]
        public void TestOrdersByUserId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Order>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Order { FirstName = "test", UserId = "testUserId" }).GetAwaiter().GetResult();
            repository.AddAsync(new Order { FirstName = "test_one", UserId = "testUserId" }).GetAwaiter().GetResult();
            repository.AddAsync(new Order { FirstName = "test_two", UserId = "testUserId_one" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var orderService = new OrdersService(repository, null);
            AutoMapperConfig.RegisterMappings(typeof(MyTestOrder).Assembly);
            var actualOrders = orderService.GetOrdersByUserId<MyTestOrder>("testUserId");

            IEnumerable<MyTestOrder> expectedOrders = new List<MyTestOrder>
            {
                new MyTestOrder { FirstName = "test", UserId = "testUserId" },
                new MyTestOrder { FirstName = "test_one", UserId = "testUserId" },
            };

            Assert.Equal<MyTestOrder>(expectedOrders, actualOrders);
        }

        public class MyTestOrder : IMapFrom<Order>, IEquatable<MyTestOrder>
        {
            public string FirstName { get; set; }

            public string UserId { get; set; }

            public bool Equals([AllowNull] MyTestOrder other)
            {
                return this.FirstName == other.FirstName && this.UserId == other.UserId;
            }
        }
    }
}
