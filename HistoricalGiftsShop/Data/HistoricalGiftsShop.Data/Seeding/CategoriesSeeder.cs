namespace HistoricalGiftsShop.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HistoricalGiftsShop.Data.Models;

    internal class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            List<Category> categories = new List<Category>();
            categories.Add(new Category
            {
                Name = "Картини",
                Title = "Картини",
                Description = "Исторически картини",
                ImageUrl = "https://www.vasilgoranov.com/paintings/historical-romanticism/tsar-simeon-zlatniyat-vek_home.jpg",
            });
            categories.Add(new Category
            {
                Name = "Книги",
                Title = "Книги",
                Description = "Исторически книги",
                ImageUrl = "https://tokorazisto.files.wordpress.com/2013/09/1-tohol_small1.jpg",
            });
            //categories.Add(new Category
            //{
            //    Name = "Керамика",
            //    Title = "Керамика",
            //    Description = "Българска керамика",
            //    ImageUrl = "https://lh3.googleusercontent.com/proxy/owyruLO893iW_xtukN-Jhb16hDDxfNzU_3ycWqgqOWhGp-XSCe4X2hFFJIqPU9LBeXGfgBYnRXO2kYg7no5DKsvMr7nqXSi7sUbLGDYXJC6LsMm7FnWxrV2lTBa4yyP4adrk4tuW",
            //});

            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
