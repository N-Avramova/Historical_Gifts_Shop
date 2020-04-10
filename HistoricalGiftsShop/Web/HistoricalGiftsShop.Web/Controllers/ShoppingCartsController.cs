namespace HistoricalGiftsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Web.ViewModels.Categories;
    using HistoricalGiftsShop.Web.ViewModels.ShoppingCarts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartItemsService shoppingCartService;
        private readonly IDeletableEntityRepository<ShoppingCartItem> repository;

        public ShoppingCartsController(
            IShoppingCartItemsService shoppingCartService,
            IDeletableEntityRepository<ShoppingCartItem> repository)
        {
            this.shoppingCartService = shoppingCartService;
            this.repository = repository;
        }

        [HttpGet]
        [Route("/ShoppingCart/AddToCart/{id}/{returnUrl?}")]
        public IActionResult AddToCart(string id, int? amount = 1, string returnUrl = null)
        {
            ISession session = this.HttpContext.Session;
            string shoppingCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shoppingCartId);

            Task<bool> isValidAmount = this.shoppingCartService.AddToCart(shoppingCartId, id, amount.Value);
            return this.Index(shoppingCartId, isValidAmount.Result, returnUrl);
        }

        public IActionResult Index(string shoppingCartId, bool isValidAmount = true, string returnUrl = "/")
        {
            return this.View("Index");
        }

        public IActionResult Back(string returnUrl = "/")
        {
            return this.Redirect(returnUrl);
        }
    }
}
