// ReSharper disable VirtualMemberCallInConstructor
namespace HistoricalGiftsShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using HistoricalGiftsShop.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.BoughtPaintings = new HashSet<Painting>();
            this.BoughtBooks = new HashSet<Book>();
            this.BoughtCeramic = new HashSet<Ceramic>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Painting> BoughtPaintings { get; set; }

        public virtual ICollection<Book> BoughtBooks { get; set; }

        public virtual ICollection<Ceramic> BoughtCeramic { get; set; }
    }
}
