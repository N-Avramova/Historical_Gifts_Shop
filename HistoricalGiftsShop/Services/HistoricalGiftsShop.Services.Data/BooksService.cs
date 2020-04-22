namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class BooksService : IBooksService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;

        public BooksService(IDeletableEntityRepository<Book> booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        public async Task<int> CreateAsync(string title, string description, string author, string publisher, DateTime yearOfPublisher, int categoryId, int stock, decimal price, int bookCoverTypeId, int? pages, string language, string isbn, string imageUrl)
        {
            var book = new Book
            {
                Title = title,
                Description = description,
                Author = author,
                Publisher = publisher,
                YearOfPublisher = yearOfPublisher,
                CategoryId = categoryId,
                Stock = stock,
                Price = price,
                BookCoverTypeId = bookCoverTypeId,
                Pages = pages,
                Language = language,
                ISBN = isbn,
                ImageUrl = imageUrl,
            };

            await this.booksRepository.AddAsync(book);
            await this.booksRepository.SaveChangesAsync();
            return book.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var book = this.booksRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.booksRepository.Delete(book);
            await this.booksRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, string description, string author, string publisher, DateTime yearOfPublisher, int categoryId, int stock, decimal price, int bookCoverTypeId, int? pages, string language, string isbn, string imageUrl)
        {
            var book = this.booksRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (book != null)
            {
                book.Title = title;
                book.Description = description;
                book.Author = author;
                book.Publisher = publisher;
                book.YearOfPublisher = yearOfPublisher;
                book.CategoryId = categoryId;
                book.Stock = stock;
                book.Price = price;
                book.BookCoverTypeId = bookCoverTypeId;
                book.Pages = pages;
                book.Language = language;
                book.ISBN = isbn;
                book.ImageUrl = imageUrl;

                this.booksRepository.Update(book);
                await this.booksRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.booksRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetBooksByPage<T>(int? take = null, int skip = 0)
        {
            var query = this.booksRepository.All()
               .OrderByDescending(x => x.CreatedOn).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetBooksCount()
        {
            return this.booksRepository.All().Count();
        }

        public T GetById<T>(int id)
        {
            var book = this.booksRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return book;
        }

        public async void UpdateStockAsync(int id, int quantity)
        {
            var book = this.booksRepository.All().Where(x => x.Id == id).FirstOrDefault();
            book.Stock = book.Stock - quantity;
            this.booksRepository.Update(book);
            await this.booksRepository.SaveChangesAsync();
        }
    }
}
