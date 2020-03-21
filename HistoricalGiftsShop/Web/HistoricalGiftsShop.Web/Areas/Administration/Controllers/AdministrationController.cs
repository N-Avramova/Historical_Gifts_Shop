namespace HistoricalGiftsShop.Web.Areas.Administration.Controllers
{
    using HistoricalGiftsShop.Common;
    using HistoricalGiftsShop.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
