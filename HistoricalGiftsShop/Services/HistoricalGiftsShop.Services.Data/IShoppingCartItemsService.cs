namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;

    public interface IShoppingCartItemsService
    {
        IEnumerable<T> GetShoppingCartItems<T>(string shoppingCartId);

        decimal GetShoppingCartTotal(string shoppingCartId);

        int GetShoppingCartSumProduct(string shoppingCartId);

        Task<int> AddOrUpdateCartAsync(string shoppingCartId, string productId, int amount, string userId);
    }
}
