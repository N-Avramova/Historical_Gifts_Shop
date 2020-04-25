namespace HistoricalGiftsShop.Web.Controllers
{
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Web.ViewModels.Paintings;
    using Microsoft.AspNetCore.Mvc;

    public class PaintingsController : Controller
    {
        private readonly IPaintingsService paintingsService;
        private readonly IImagesService imagesService;

        public PaintingsController(
            IPaintingsService paintingsService,
            IImagesService imagesService)
        {
            this.paintingsService = paintingsService;
            this.imagesService = imagesService;
        }

        public IActionResult ById(string id)
        {
            var paintingViewModel = this.paintingsService.GetById<PaintingViewModel>(id);
            if (paintingViewModel == null)
            {
                return this.View("CustomError");
            }

            var images = this.imagesService.GetByPaintingId<ImageViewModel>(id);
            paintingViewModel.ImageUrls = images;
            return this.View(paintingViewModel);
        }
    }
}
