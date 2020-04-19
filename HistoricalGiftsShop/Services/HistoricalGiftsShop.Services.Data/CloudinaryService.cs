namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<List<string>> UploadAsync(ICollection<IFormFile> files)
        {
            List<string> resultUrls = new List<string>();

            foreach (var file in files)
            {
                byte[] destinationImage;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    destinationImage = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(destinationImage))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, destinationStream),
                    };

                    var resultUrl = await this.cloudinary.UploadAsync(uploadParams);
                    resultUrls.Add(resultUrl.Uri.AbsoluteUri);
                }
            }

            return resultUrls;
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            var deletionParams = new DeletionParams(imageUrl)
            {
                PublicId = imageUrl,
                ResourceType = ResourceType.Image,
            };

            await this.cloudinary.DestroyAsync(deletionParams);
        }
    }
}
