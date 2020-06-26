using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Membership = new HashSet<Membership>();
            Product = new HashSet<Product>();
            VendorAddress = new HashSet<VendorAddress>();
            VendorReview = new HashSet<VendorReview>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Membership> Membership { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<VendorAddress> VendorAddress { get; set; }
        public virtual ICollection<VendorReview> VendorReview { get; set; }
    }
}
