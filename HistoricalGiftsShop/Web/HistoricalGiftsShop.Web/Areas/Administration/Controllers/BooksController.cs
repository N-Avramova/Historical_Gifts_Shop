namespace HistoricalGiftsShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Administration.Books;
    using HistoricalGiftsShop.Web.ViewModels.Books;
    using HistoricalGiftsShop.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : AdministrationController
    {
        private readonly IBooksService booksService;
        private readonly ICategoriesService categoriesService;
        private readonly IBookCoverTypesService bookCoverTypesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Book> repository;

        public BooksController(
            IBooksService booksService,
            ICategoriesService categoriesService,
            IBookCoverTypesService bookCoverTypesService,
            ICloudinaryService cloudinaryService,
            IDeletableEntityRepository<Book> repository)
        {
            this.booksService = booksService;
            this.categoriesService = categoriesService;
            this.bookCoverTypesService = bookCoverTypesService;
            this.cloudinaryService = cloudinaryService;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            BookIndexViewModel viewModel = new BookIndexViewModel()
            {
                Books = this.booksService.GetAll<BookViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var category = this.categoriesService.GetByName<CategoryViewModel>("Книги");
            var bookCoverTypes = this.bookCoverTypesService.GetAll<BookCoverTypeDropDownViewModel>();
            var viewModel = new BookCreateInputModel
            {
                Category = category,
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

            List<IFormFile> files = new List<IFormFile>(1);
            files.Add(input.ImageUrl);
            var resultUrls = await this.cloudinaryService.UploadAsync(files);

            var bookId = await this.booksService.CreateAsync(input.Title, input.Description, input.Author, input.Publisher, input.YearOfPublisher, input.CategoryId, input.Stock, input.Price, input.BookCoverTypeId, input.Pages, input.Language, input.ISBN, resultUrls[0]);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        public async Task<RedirectToActionResult> Delete(int id)
        {
            await this.booksService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var bookEditModel = this.booksService.GetById<BookEditModel>(id);
            var bookCoverTypes = this.bookCoverTypesService.GetAll<BookCoverTypeDropDownViewModel>();
            if (bookEditModel == null)
            {
                return this.NotFound();
            }
            else
            {
                bookEditModel.BookCoverTypes = bookCoverTypes;
            }

            return this.View(bookEditModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(BookEditModel input)
        {
            var book = AutoMapperConfig.MapperInstance.Map<Book>(input);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string imageUrl = book.ImageUrl;
            if (input.ChangedImageUrl != null)
            {
                List<IFormFile> files = new List<IFormFile>(1);
                files.Add(input.ChangedImageUrl);
                var resultUrls = await this.cloudinaryService.UploadAsync(files);
                imageUrl = resultUrls[0];
            }

            await this.booksService.EditAsync(input.Id, input.Title, input.Description, input.Author, input.Publisher, input.YearOfPublisher, input.CategoryId, input.Stock, input.Price, input.BookCoverTypeId, input.Pages, input.Language, input.ISBN, imageUrl);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
