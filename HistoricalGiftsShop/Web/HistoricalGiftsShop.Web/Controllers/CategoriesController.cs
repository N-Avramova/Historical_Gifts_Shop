namespace HistoricalGiftsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Web.ViewModels.Categories;
    using HistoricalGiftsShop.Web.ViewModels.Paintings;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 8;

        private readonly ICategoriesService categoriesService;
        private readonly IPaintingsService paintingsService;
        private readonly IBooksService booksService;

        public CategoriesController(ICategoriesService categoriesService, IPaintingsService paintingsService, IBooksService booksService)
        {
            this.categoriesService = categoriesService;
            this.paintingsService = paintingsService;
            this.booksService = booksService;
        }

        public IActionResult ByName(string name, int page = 1)
        {
            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            if (viewModel == null)
            {
                return this.View("CustomError");
            }

            var count = 0;
            if (name == "Картини")
            {
                viewModel.Paintings = this.paintingsService.GetPaintingsByPage<ProductInCategoryViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);
                count = this.paintingsService.GetPaintingsCount();
            }
            else if (name == "Книги")
            {
                viewModel.Books = this.booksService.GetBooksByPage<ProductInCategoryViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);
                count = this.booksService.GetBooksCount();
            }

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult Search(string searchString, int page)
        {
            List<CategoryViewModel> categories = this.categoriesService.GetAll<CategoryViewModel>().ToList();

            foreach (CategoryViewModel item in categories)
            {
                if (item.Name == "Картини")
                {
                    item.Paintings = this.paintingsService.GetPaintingsBySearchString<ProductInCategoryViewModel>(searchString);
                }
                else if (item.Name == "Книги")
                {
                    item.Books = this.booksService.GetBooksBySearchString<ProductInCategoryViewModel>(searchString);
                }
            }

            var viewModel = new AllCategoriesViewModel()
            {
                SearchString = searchString,
                Categories = categories,
            };

            if (viewModel == null)
            {
                return this.View("CustomError");
            }

            return this.View("Search", viewModel);
        }
    }
}
