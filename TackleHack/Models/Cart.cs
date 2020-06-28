using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public string UserName{ get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Product Product { get; set; }
    }
}
