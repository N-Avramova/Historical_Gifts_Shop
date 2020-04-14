namespace HistoricalGiftsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Web.ViewModels.ShoppingCarts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCartAsync(AddOrUpdateCartViewModel updateCart)
        {
            ISession session = this.HttpContext.Session;
            string shoppingCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shoppingCartId);

            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user?.Id;

            int currentAmount = await this.shoppingCartService.AddOrUpdateCartAsync(shoppingCartId, updateCart.Id, updateCart.Quantity, userId);
            return this.Json(currentAmount);
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = "/")
        {
            ISession session = this.HttpContext.Session;
            string shoppingCartId = session.GetString("CartId");

            IEnumerable<ShoppingCartItemViewModel> shoppingCartItems = this.shoppingCartService.GetShoppingCartItems<ShoppingCartItemViewModel>(shoppingCartId);
            decimal shoppingCartTotal = this.shoppingCartService.GetShoppingCartTotal(shoppingCartId);

            returnUrl = returnUrl.Replace("%2F", "/");

            var viewModel = new ShoppingCartViewModel
            {
                ShoppingCartItems = shoppingCartItems,
                ShoppingCartTotal = shoppingCartTotal,
                ReturnUrl = returnUrl,
            };

            return this.View("Index", viewModel);
        }
    }
}
