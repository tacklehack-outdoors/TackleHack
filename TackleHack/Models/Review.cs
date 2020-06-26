using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Review
    {
        public Review()
        {
            ProductReview = new HashSet<ProductReview>();
            VendorReview = new HashSet<VendorReview>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<ProductReview> ProductReview { get; set; }
        public virtual ICollection<VendorReview> VendorReview { get; set; }
    }
}
