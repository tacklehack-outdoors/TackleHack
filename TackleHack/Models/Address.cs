using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Address
    {
        public Address()
        {
            UserAddress = new HashSet<UserAddress>();
            VendorAddress = new HashSet<VendorAddress>();
        }

        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<UserAddress> UserAddress { get; set; }
        public virtual ICollection<VendorAddress> VendorAddress { get; set; }
    }
}
