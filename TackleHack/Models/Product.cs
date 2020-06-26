using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Product
    {
        public Product()
        {
            Cart = new HashSet<Cart>();
            Media = new HashSet<Media>();
            ProductFeature = new HashSet<ProductFeature>();
            ProductReview = new HashSet<ProductReview>();
            Tag = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public string ItemNumber { get; set; }
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public double Msrp { get; set; }
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
        public virtual ICollection<ProductReview> ProductReview { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
    }
}
