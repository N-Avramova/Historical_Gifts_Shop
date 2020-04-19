namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Common.Repositories;
    using HistoricalGiftsShop.Data.Models;
    using HistoricalGiftsShop.Services.Mapping;

    public class ImagesService : IImagesService
    {

        public readonly IDeletableEntityRepository<Images> imagesRepository;

        public ImagesService(IDeletableEntityRepository<Images> imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }

        public async Task CreateAsync(string paintingId, bool coverImage, string imageUrl)
        {
            var image = new Images
            {
                PaintingId = paintingId,
                CoverImage = coverImage,
                ImageUrl = imageUrl,
            };

            await this.imagesRepository.AddAsync(image);
            await this.imagesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string paintingId, string deleteImageUrl)
        {
            var imageForDeleted = this.imagesRepository.All().Where(x => x.PaintingId == paintingId && x.ImageUrl == deleteImageUrl).FirstOrDefault();
            this.imagesRepository.Delete(imageForDeleted);
            await this.imagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetByPaintingId<T>(string id)
        {
            return this.imagesRepository.All().Where(x => x.PaintingId == id).To<T>().ToList();
        }

        public async Task UpdateAsync(string paintingId, string newCoverImageUrl)
        {
            var oldCoverImage = this.imagesRepository.All().Where(x => x.PaintingId == paintingId && x.CoverImage == true).FirstOrDefault();
            oldCoverImage.CoverImage = false;
            this.imagesRepository.Update(oldCoverImage);
            await this.imagesRepository.SaveChangesAsync();

            await this.CreateAsync(paintingId, true, newCoverImageUrl);
        }
    }
}
