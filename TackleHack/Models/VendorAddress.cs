using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class VendorAddress
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
