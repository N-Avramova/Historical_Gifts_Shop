namespace HistoricalGiftsShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using HistoricalGiftsShop.Data.Common.Models;

    public class Painting : BaseDeletableModel<string>
    {
        public Painting()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets name of painting.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description of painting.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets category.
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or sets stock.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets price of painting.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or Sets code of painting.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets length of painting.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets width of painting.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets height of painting.
        /// </summary>
        public int Height { get; set; }

        public PaintingType Paint { get; set; }

        public ICollection<Images> ImageUrls { get; set; }
    }
}
