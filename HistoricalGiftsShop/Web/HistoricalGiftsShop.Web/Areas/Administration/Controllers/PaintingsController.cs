namespace HistoricalGiftsShop.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Data;
    using HistoricalGiftsShop.Services.Mapping;
    using HistoricalGiftsShop.Web.ViewModels.Administration.Paintings;
    using HistoricalGiftsShop.Web.ViewModels.Categories;
    using HistoricalGiftsShop.Web.ViewModels.Paintings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class PaintingsController : AdministrationController
    {
        private readonly IPaintingsService paintingsService;
        private readonly ICategoriesService categoriesService;
        private readonly IImagesService imagesService;
        private readonly ICloudinaryService cloudinaryService;

        public PaintingsController(
            IPaintingsService paintingsService,
            ICategoriesService categoriesService,
            IImagesService imagesService,
            ICloudinaryService cloudinaryService)
        {
            this.paintingsService = paintingsService;
            this.categoriesService = categoriesService;
            this.imagesService = imagesService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Index()
        {
            PaintingIndexViewModel viewModel = new PaintingIndexViewModel()
            {
                Paintings = this.paintingsService.GetAll<PaintingViewModel>(),
            };

            return this.View(viewModel);
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

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        public async Task<RedirectToActionResult> Delete(string id)
        {
            await this.paintingsService.DeleteByIdAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var paintingEditModel = this.paintingsService.GetById<PaintingEditModel>(id);
            if (paintingEditModel == null)
            {
                return this.View("CustomError");
            }

            var images = this.imagesService.GetByPaintingId<ImageViewModel>(id);
            paintingEditModel.ImageUrls = images;
            return this.View(paintingEditModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(PaintingEditModel input)
        {
            var painting = AutoMapperConfig.MapperInstance.Map<Painting>(input);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.paintingsService.EditAsync(input.Id, input.Name, input.Description, input.Author, input.CategoryId, input.Stock, input.Price, input.Code, input.Length, input.Width, input.Height, input.Paint);

            return this.RedirectToAction(nameof(this.Index));
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeCoverImage(PaintingEditModel input)
        {
            if (input.ActionType == "add")
            {
                if (input.AdditionalImageUrls != null)
                {
                    List<IFormFile> files = new List<IFormFile>(1);

                    files = new List<IFormFile>(input.AdditionalImageUrls);
                    var resultUrls = await this.cloudinaryService.UploadAsync(files);

                    foreach (string imageUrl in resultUrls)
                    {
                        await this.imagesService.CreateAsync(input.Id, false, imageUrl);
                    }
                }

            }
            else if (input.ActionType == "change")
            {
                if (input.CoverImageUrl != null)
                {
                    List<IFormFile> files = new List<IFormFile>(1);
                    files.Add(input.CoverImageUrl);
                    var resultUrls = await this.cloudinaryService.UploadAsync(files);
                    foreach (string imageUrl in resultUrls)
                    {
                        await this.imagesService.UpdateAsync(input.Id, imageUrl);
                    }
                }
            }
            else if (input.ActionType == "delete")
            {
                if (!string.IsNullOrWhiteSpace(input.ImageUrlForDelete))
                {
                    await this.cloudinaryService.DeleteImageAsync(input.ImageUrlForDelete);
                    await this.imagesService.DeleteAsync(input.Id, input.ImageUrlForDelete);
                }
            }

            return this.RedirectToAction(nameof(this.Edit), new { id = input.Id });
        }
    }
}
