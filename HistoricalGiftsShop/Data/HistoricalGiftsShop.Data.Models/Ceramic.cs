namespace HistoricalGiftsShop.Data.Models
{
    using System.Collections.Generic;

    using HistoricalGiftsShop.Data.Common.Models;

    public class Ceramic : BaseDeletableModel<int>
    {
        /// <summary>
        /// Gets or sets name of ceramic product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description of ceramic product.
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
        /// Gets or sets price of ceramic product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or Sets code of ceramic product.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets capacity.
        /// </summary>
        public int Capacity { get; set; }

        public string Measure { get; set; }

        public ICollection<Images> ImageUrls { get; set; }
    }
}
