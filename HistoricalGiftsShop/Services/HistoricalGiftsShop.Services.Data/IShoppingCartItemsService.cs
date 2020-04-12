namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;

    public interface IShoppingCartItemsService
    {
        Task<bool> AddToCart(string shoppingCartId, string productId, int amount, string userId);

        IEnumerable<T> GetShoppingCartItems<T>(string shoppingCartId);

        decimal GetShoppingCartTotal(string shoppingCartId);

        void RemoveFromCart(string shoppingCartId, string productId);

        Task<bool> ChangeAmount(string shoppingCartId, string productId, bool isUpAmount);
    }
}
