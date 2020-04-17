namespace HistoricalGiftsShop.Web.Controllers
{
    using System.Diagnostics;

    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Web.ViewModels;
    using HistoricalGiftsShop.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            ISession session = this.HttpContext.Session;
            if (session.GetString("ShoppingCartSumProduct") == null)
            {
                session.SetString("ShoppingCartSumProduct", "0 продукт(и) - 0.00лв");
            }

            var viewModel = new IndexViewModel
            {
                Categories =
                    this.categoriesService.GetAll<IndexCategoryViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
