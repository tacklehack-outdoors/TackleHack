using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
