namespace HistoricalGiftsShop.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data;
    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ShoppingCartItemsServiceTests
    {
        [Fact]
        public void GetShoppingCartSumShouldReturnCorrectNumber()
        {
            string shoppingCartId = Guid.NewGuid().ToString();
            var repository = new Mock<IDeletableEntityRepository<ShoppingCartItem>>();
            repository.Setup(r => r.All()).Returns(new List<ShoppingCartItem>
                                                        {
                                                            new ShoppingCartItem() { ShoppingCartId = shoppingCartId, Amount = 2 },
                                                            new ShoppingCartItem() { ShoppingCartId = shoppingCartId, Amount = 1 },
                                                            new ShoppingCartItem() { ShoppingCartId = shoppingCartId, Amount = 2 },
                                                            new ShoppingCartItem() { ShoppingCartId = "otherValue", Amount = 2 },
                                                        }.AsQueryable());

            var service = new ShoppingCartItemsService(null, null, repository.Object);
            Assert.Equal(5, service.GetShoppingCartSumProduct(shoppingCartId));
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetShoppingCartSumShouldReturnCorrectNumberUsingDbContext()
        {
            string shoppingCartId = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShoppingCartItemsTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { ShoppingCartId = shoppingCartId, Amount = 2 });
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { ShoppingCartId = shoppingCartId, Amount = 1 });
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { ShoppingCartId = shoppingCartId, Amount = 2 });
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { ShoppingCartId = "otherValue", Amount = 2 });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<ShoppingCartItem>(dbContext);
            var service = new ShoppingCartItemsService(null, null, repository);
            Assert.Equal(5, service.GetShoppingCartSumProduct(shoppingCartId));
        }
    }
}
