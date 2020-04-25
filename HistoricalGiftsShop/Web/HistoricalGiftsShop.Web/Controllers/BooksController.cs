namespace HistoricalGiftsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Web.ViewModels.Books;

    using Microsoft.AspNetCore.Mvc;

    public class BooksController : Controller
    {
        private readonly IBooksService booksService;

        public BooksController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        public IActionResult ById(int id)
        {
            var bookViewModel = this.booksService.GetById<BookViewModel>(id);
            if (bookViewModel == null)
            {
                return this.View("CustomError");
            }

            return this.View(bookViewModel);
        }
    }
}