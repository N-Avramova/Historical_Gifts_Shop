namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ShoppingCartItemsService : IShoppingCartItemsService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;
        public readonly IDeletableEntityRepository<Painting> paintingsRepository;
        private readonly IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemsRepository;

        public ShoppingCartItemsService(
            IDeletableEntityRepository<Book> booksRepository,
            IDeletableEntityRepository<Painting> paintingsRepository,
            IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemsRepository)
        {
            this.booksRepository = booksRepository;
            this.paintingsRepository = paintingsRepository;
            this.shoppingCartItemsRepository = shoppingCartItemsRepository;
        }

        public async Task<bool> AddToCart(string shoppingCartId, string productId, int amount)
        {
            Book book = null;
            Painting painting = null;
            ShoppingCartItem shoppingCartItem = null;
            int stock;

            int bookId = 0;
            if (int.TryParse(productId, out bookId))
            {
                book = this.booksRepository.All().Where(x => x.Id == bookId).SingleOrDefault();
                stock = book.Stock;
                shoppingCartItem = this.shoppingCartItemsRepository.All().Include(b => b.Book).SingleOrDefault(x => x.ShoppingCartId == shoppingCartId && x.Book.Id == bookId);
            }
            else
            {
                painting = this.paintingsRepository.All().Where(x => x.Id == productId).SingleOrDefault();
                stock = painting.Stock;
                shoppingCartItem = this.shoppingCartItemsRepository.All().Include(p => p.Painting).SingleOrDefault(x => x.ShoppingCartId == shoppingCartId && x.Painting.Id == productId);
            }

            if ((book == null || painting == null) && amount == 0)
            {
                return false;
            }

            var isValidAmount = true;
            if (shoppingCartItem == null)
            {
                if (amount > stock)
                {
                    isValidAmount = false;
                }

                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = shoppingCartId,
                    Book = book,
                    Painting = painting,
                    Amount = Math.Min(stock, amount),
                };

                await this.shoppingCartItemsRepository.AddAsync(shoppingCartItem);
            }
            else
            {
                if (stock - shoppingCartItem.Amount - amount >= 0)
                {
                    shoppingCartItem.Amount += amount;
                }
                else
                {
                    shoppingCartItem.Amount += stock - shoppingCartItem.Amount;
                    isValidAmount = false;
                }

                this.shoppingCartItemsRepository.Update(shoppingCartItem);
            }

            await this.shoppingCartItemsRepository.SaveChangesAsync();
            return isValidAmount;
        }
    }
}
