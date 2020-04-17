namespace HistoricalGiftsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Common;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Services.Messaging;
    using HistoricalGiftsShop.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        private readonly IShoppingCartItemsService shoppingCartItemsService;
        private readonly IOrdersService ordersService;
        private readonly IPaintingsService paintingsService;
        private readonly IBooksService booksService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public OrdersController(
            IShoppingCartItemsService shoppingCartItemsService,
            IOrdersService ordersService,
            IPaintingsService paintingsService,
            IBooksService booksService,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            this.shoppingCartItemsService = shoppingCartItemsService;
            this.ordersService = ordersService;
            this.paintingsService = paintingsService;
            this.booksService = booksService;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [Authorize]
        public IActionResult Create()
        {
            ISession session = this.HttpContext.Session;
            string shoppingCartId = session.GetString("CartId");

            IEnumerable<OrderDetailsViewModel> orderDetails = this.shoppingCartItemsService.GetShoppingCartItems<OrderDetailsViewModel>(shoppingCartId);
            decimal shoppingCartTotal = this.shoppingCartItemsService.GetShoppingCartTotal(shoppingCartId);

            var order = new OrderCreateInputModel
            {
                OrderDetails = orderDetails.ToList(),
                OrderTotal = shoppingCartTotal,
            };

            return this.View(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(OrderCreateInputModel input)
        {
            ISession session = this.HttpContext.Session;
            string shoppingCartId = session.GetString("CartId");

            IEnumerable<OrderDetailsViewModel> orderDetails = this.shoppingCartItemsService.GetShoppingCartItems<OrderDetailsViewModel>(shoppingCartId);
            input.OrderDetails = orderDetails;
            var order = AutoMapperConfig.MapperInstance.Map<Order>(input);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user?.Id;

            var orderId = await this.ordersService.CreateOrderAsync(input.FirstName, input.LastName, input.Email, input.Phone, input.Address, input.Country, input.City, userId, input.OrderTotal);

            foreach (OrderDetailsViewModel item in input.OrderDetails)
            {
                await this.ordersService.CreateOrderDetailsAsync(orderId, item.Book, item.Painting, item.Amount, item.Price);
                if (item.Painting != null)
                {
                    this.paintingsService.UpdateStockAsync(item.Painting.Id, item.Amount);
                }

                if (item.Book != null)
                {
                    this.booksService.UpdateStockAsync(item.Book.Id, item.Amount);
                }
            }

            string htmlContent = string.Format(
                        @" <span>Поръчка номер <b>{0}</b></span> 
                        <span> от <b> {1} {2} </b></span> <br> Детайли за поръчката: .....",
                        orderId, order.FirstName, order.LastName);

            await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, GlobalConstants.SystemName, input.Email, "Информация за поръчка номер " + orderId.ToString(), htmlContent);

            session.Remove("CartId");
            session.SetString("ShoppingCartSumProduct", "0 продукт(и) - 0.00лв");

            return this.View("Completed");
        }

        [Authorize]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string userId = user?.Id;

            IEnumerable<OrderViewModel> orders = this.ordersService.GetOrdersByUserId<OrderViewModel>(userId);

            foreach (OrderViewModel item in orders)
            {
                item.OrderDetails = this.ordersService.GetOrderDetailsByOrderId<OrderDetailsViewModel>(item.Id);
            }

            var viewModel = new AllOrdersViewModel
            {
                Orders = orders,
            };

            return this.View("GetOrders", viewModel);
        }
    }
}
