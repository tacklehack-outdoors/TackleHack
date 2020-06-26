using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class UserAddress
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
    }
}
