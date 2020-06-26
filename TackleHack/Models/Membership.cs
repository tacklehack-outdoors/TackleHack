using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Membership
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int VendorId { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
