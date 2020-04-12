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
        private readonly IRepository<ShoppingCartItem> shoppingCartItemsRepository;

        public ShoppingCartItemsService(
            IDeletableEntityRepository<Book> booksRepository,
            IDeletableEntityRepository<Painting> paintingsRepository,
            IRepository<ShoppingCartItem> shoppingCartItemsRepository)
        {
            this.booksRepository = booksRepository;
            this.paintingsRepository = paintingsRepository;
            this.shoppingCartItemsRepository = shoppingCartItemsRepository;
        }

        public async Task<bool> AddToCart(string shoppingCartId, string productId, int amount, string userId)
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
                    UserId = userId,
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

        public decimal GetShoppingCartTotal(string shoppingCartId)
        {
            var bookPrice = this.shoppingCartItemsRepository.All().Where(x => x.ShoppingCartId == shoppingCartId).Select(c => c.Book.Price * c.Amount).Sum();
            var paintingPrice = this.shoppingCartItemsRepository.All().Where(x => x.ShoppingCartId == shoppingCartId).Select(c => c.Painting.Price * c.Amount).Sum();

            return bookPrice + paintingPrice;
        }

        public IEnumerable<T> GetShoppingCartItems<T>(string shoppingCartId)
        {
            return this.shoppingCartItemsRepository.All().Include(b => b.Book).Include(p => p.Painting).Where(x => x.ShoppingCartId == shoppingCartId).To<T>().ToList();
        }

        public async void RemoveFromCart(string shoppingCartId, string productId)
        {
            ShoppingCartItem shoppingCartItem = null;
            int bookId = 0;

            if (int.TryParse(productId, out bookId))
            {
                shoppingCartItem = this.shoppingCartItemsRepository.All().SingleOrDefault(x => x.ShoppingCartId == shoppingCartId && x.Book.Id == bookId);
            }
            else
            {
                shoppingCartItem = this.shoppingCartItemsRepository.All().SingleOrDefault(x => x.ShoppingCartId == shoppingCartId && x.Painting.Id == productId);
            }

            if (shoppingCartItem != null)
            {
                this.shoppingCartItemsRepository.Delete(shoppingCartItem);
            }

            await this.shoppingCartItemsRepository.SaveChangesAsync();
        }

        public async Task<bool> ChangeAmount(string shoppingCartId, string productId, bool isUpAmount)
        {
            Book book = null;
            Painting painting = null;
            ShoppingCartItem shoppingCartItem = null;
            int stock;
            bool isValidAmount = true;

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

            if (shoppingCartItem != null)
            {
                if (isUpAmount)
                {
                    if (stock - shoppingCartItem.Amount - 1 >= 0)
                    {
                        shoppingCartItem.Amount += 1;
                        this.shoppingCartItemsRepository.Update(shoppingCartItem);
                    }
                    else
                    {
                        isValidAmount = false;
                    }
                }
                else
                {
                    if (shoppingCartItem.Amount > 1)
                    {
                        shoppingCartItem.Amount -= 1;
                        this.shoppingCartItemsRepository.Update(shoppingCartItem);
                    }
                    else
                    {
                        shoppingCartItem.Amount = 0;
                        this.shoppingCartItemsRepository.Delete(shoppingCartItem);
                    }
                }

                await this.shoppingCartItemsRepository.SaveChangesAsync();
            }

            return isValidAmount;
        }
    }
}
