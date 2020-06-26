using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
