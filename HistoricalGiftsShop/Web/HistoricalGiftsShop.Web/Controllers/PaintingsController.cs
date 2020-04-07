namespace HistoricalGiftsShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Categories;
    using HistoricalGiftsShop.Web.ViewModels.Paintings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class PaintingsController : Controller
    {
        private readonly IPaintingsService paintingsService;
        private readonly ICategoriesService categoriesService;
        private readonly IImagesService imagesService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<Painting> repository;

        public PaintingsController(
            IPaintingsService paintingsService,
            ICategoriesService categoriesService,
            IImagesService imagesService,
            ICloudinaryService cloudinaryService,
            IDeletableEntityRepository<Painting> repository)
        {
            this.paintingsService = paintingsService;
            this.categoriesService = categoriesService;
            this.imagesService = imagesService;
            this.cloudinaryService = cloudinaryService;
            this.repository = repository;
        }

        public IActionResult ById(string id)
        {
            var paintingViewModel = this.paintingsService.GetById<PaintingViewModel>(id);
            if (paintingViewModel == null)
            {
                return this.NotFound();
            }

            var images = this.imagesService.GetByPaintingId<ImageViewModel>(id);
            paintingViewModel.ImageUrls = images;
            return this.View(paintingViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var category = this.categoriesService.GetByName<CategoryViewModel>("Картини");
            var viewModel = new PaintingCreateInputModel
            {
                Category = category,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PaintingCreateInputModel input)
        {
            var painting = AutoMapperConfig.MapperInstance.Map<Painting>(input);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var paintingId = await this.paintingsService.CreateAsync(input.Name, input.Description, input.Author, input.CategoryId, input.Stock, input.Price, input.Code, input.Length, input.Width, input.Height, input.Paint);

            List<IFormFile> files = new List<IFormFile>(1);
            files.Add(input.CoverImageUrl);
            var resultUrls = await this.cloudinaryService.UploadAsync(files);
            foreach (string imageUrl in resultUrls)
            {
                await this.imagesService.CreateAsync(paintingId, true, imageUrl);
            }

            if (input.AdditionalImageUrls != null)
            {
                files = new List<IFormFile>(input.AdditionalImageUrls);
                resultUrls = await this.cloudinaryService.UploadAsync(files);

                foreach (string imageUrl in resultUrls)
                {
                    await this.imagesService.CreateAsync(paintingId, false, imageUrl);
                }
            }

            return this.RedirectToAction(nameof(this.ById), new { id = paintingId });
        }
    }
}
