namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;

    public interface IShoppingCartItemsService
    {
        Task<bool> AddToCart(string shoppingCartId, string productId, int amount);
    }
}
