using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class ProductFeature
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FeatureId { get; set; }

        public virtual Feature Feature { get; set; }
        public virtual Product Product { get; set; }
    }
}
