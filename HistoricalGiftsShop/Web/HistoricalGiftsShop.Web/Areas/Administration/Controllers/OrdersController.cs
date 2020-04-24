namespace HistoricalGiftsShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Common;
    using HistoricalGiftsShop.Common.Enums;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Administration.Orders;
    using HistoricalGiftsShop.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IOrdersService ordersService;

        public OrdersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOrdersService ordersService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.ordersService = ordersService;
        }

        [Route("/Index")]
        public IActionResult Index()
        {
            ApplicationRole role = this.roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName).Result;
            var usersAndRoles = this.userManager.Users.Include(x => x.Roles).Where(x => x.Roles.Count() == 0).ToList();
            var result = AutoMapperConfig.MapperInstance.Map<IEnumerable<UserViewModel>>(usersAndRoles);

            var usersOrdersViewModel = new UsersOrdersViewModel
            {
                AllUsers = result,
            };

            return this.View(usersOrdersViewModel);
        }

        [Route("/IndexChange")]
        [Authorize]
        public async Task<IActionResult> IndexChange(string id)
        {
            ApplicationRole role = this.roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName).Result;
            var usersAndRoles = this.userManager.Users.Include(x => x.Roles).Where(x => x.Roles.Count() == 0).ToList();
            var result = AutoMapperConfig.MapperInstance.Map<IEnumerable<UserViewModel>>(usersAndRoles);

            IEnumerable<OrderViewModel> orders = null;

            if (id != "-1")
            {
                orders = this.ordersService.GetOrdersByUserId<OrderViewModel>(id);
                foreach (OrderViewModel item in orders)
                {
                    item.OrderDetails = this.ordersService.GetOrderDetailsByOrderId<OrderDetailsViewModel>(item.Id);
                }
            }

            var viewModel = new UsersOrdersViewModel
            {
                AllUsers = result,
                Orders = orders,
                UserId = id,
            };

            return this.View("Index", viewModel);
        }

        [HttpPost]
        public async Task ChangeStatus(ChangeStatusViewModel updateOrder)
        {
            Enum.TryParse(updateOrder.Type, out OrderStatusType orderStatus);
            await this.ordersService.UpdateOrderStatusAsync(updateOrder.Id, orderStatus);
        }
    }
}
