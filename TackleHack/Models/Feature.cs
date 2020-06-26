using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Feature
    {
        public Feature()
        {
            ProductFeature = new HashSet<ProductFeature>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductFeature> ProductFeature { get; set; }
    }
}
