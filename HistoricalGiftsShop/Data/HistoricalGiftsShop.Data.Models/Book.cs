namespace HistoricalGiftsShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    using HistoricalGiftsShop.Data.Common.Models;

    public class Book : BaseDeletableModel<int>
    {
        /// <summary>
        /// Gets or sets title of book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of book.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets author ot book.
        /// </summary>
        public string Author { get; set; }

        public string Publisher { get; set; }

        public DateTime YearOfPublisher { get; set; }

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

        public int BookCoverTypeId { get; set; }

        public BookCoverType CoverType { get; set; }

        // if paperback and hardback then page count else null
        public int? Pages { get; set; }

        public string Language { get; set; }

        // 13 digits
        // example : 978-6191710928
        // http://nationallibrary.bg/wp/?page_id=5077#what
        [RegularExpression("[0-9]{3}-[0-9]{10}")]
        public string ISBN { get; set; }
    }
}
