namespace HistoricalGiftsShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HistoricalGiftsShop.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Paintings = new HashSet<Painting>();
            this.Books = new HashSet<Book>();
            this.Ceramics = new HashSet<Ceramic>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? CategoryId { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<Ceramic> Ceramics { get; set; }
    }
}
