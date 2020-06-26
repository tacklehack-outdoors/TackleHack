using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class ProductReview
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Review Review { get; set; }
    }
}
