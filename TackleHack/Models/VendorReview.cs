using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class VendorReview
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int VendorId { get; set; }

        public virtual Review Review { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
