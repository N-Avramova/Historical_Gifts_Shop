namespace HistoricalGiftsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Books;
    using HistoricalGiftsShop.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly ICategoriesService categoriesService;
        private readonly IBookCoverTypesService bookCoverTypesService;
        private readonly IDeletableEntityRepository<Book> repository;

        public BooksController(
            IBooksService booksService,
            ICategoriesService categoriesService,
            IBookCoverTypesService bookCoverTypesService,
            IDeletableEntityRepository<Book> repository)
        {
            this.booksService = booksService;
            this.categoriesService = categoriesService;
            this.bookCoverTypesService = bookCoverTypesService;
            this.repository = repository;
        }

        public IActionResult ById(int id)
        {
            var bookViewModel = this.booksService.GetById<BookViewModel>(id);
            if (bookViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(bookViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var category = this.categoriesService.GetByName<CategoryViewModel>("Книги");
            var bookCoverTypes = this.bookCoverTypesService.GetAll<BookCoverTypeDropDownViewModel>();
            var viewModel = new BookCreateInputModel
            {
                CategoryId = category.Id,
                BookCoverTypes = bookCoverTypes,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BookCreateInputModel input)
        {
            var book = AutoMapperConfig.MapperInstance.Map<Book>(input);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var bookId = await this.booksService.CreateAsync(input.Title, input.Description, input.Author, input.Publisher, input.YearOfPublisher, input.CategoryId, input.Stock, input.Price, input.BookCoverTypeId, input.Pages, input.Language, input.ISBN, input.ImageUrl.FileName);
            return this.RedirectToAction(nameof(this.ById), new { id = bookId });
        }
    }
}