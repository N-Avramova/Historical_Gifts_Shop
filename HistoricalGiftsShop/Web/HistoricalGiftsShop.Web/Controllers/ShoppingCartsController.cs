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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartItemsService shoppingCartService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartsController(
            IShoppingCartItemsService shoppingCartService,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartService = shoppingCartService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("/ShoppingCarts/AddToCart/{id}/{returnUrl?}")]
        public async Task<IActionResult> AddToCart(string id, int? amount = 1, string returnUrl = null)
        {
            ISession session = this.HttpContext.Session;
            string shoppingCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shoppingCartId);

            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user?.Id;

            Task<bool> isValidAmount = this.shoppingCartService.AddToCart(shoppingCartId, id, amount.Value, userId);
            return this.Index(shoppingCartId, isValidAmount.Result, returnUrl);
        }

        public IActionResult Index(string shoppingCartId, bool isValidAmount = true, string returnUrl = "/")
        {
            IEnumerable<ShoppingCartItemViewModel> shoppingCartItems = this.shoppingCartService.GetShoppingCartItems<ShoppingCartItemViewModel>(shoppingCartId);
            decimal shoppingCartTotal = this.shoppingCartService.GetShoppingCartTotal(shoppingCartId);

            var viewModel = new ShoppingCartViewModel
            {
                ShoppingCartItems = shoppingCartItems,
                ShoppingCartTotal = shoppingCartTotal,
                ReturnUrl = returnUrl,
            };

            if (!isValidAmount)
            {
                this.TempData["NotEnoughItems"] = "There were not enough items in stock to add.";
            }

            return this.View("Index", viewModel);
        }

        public IActionResult Back(string returnUrl = "/")
        {
            returnUrl = returnUrl.Replace("%2F", "/");
            return this.Redirect(returnUrl);
        }

        public IActionResult Remove(string productId)
        {
            ISession session = this.HttpContext.Session;
            string shoppingCartId = session.GetString("CartId");

            if (shoppingCartId != null)
            {
                this.shoppingCartService.RemoveFromCart(shoppingCartId, productId);
            }

            return this.Index(shoppingCartId, true, null);
        }

        [Authorize]
        public IActionResult ChangeAmount(string productId, bool isUpAmount)
        {
            ISession session = this.HttpContext.Session;
            string shoppingCartId = session.GetString("CartId");

            Task<bool> isValidAmount = this.shoppingCartService.ChangeAmount(shoppingCartId, productId, isUpAmount);

            return this.Index(shoppingCartId, isValidAmount.Result, null);
        }
    }
}
