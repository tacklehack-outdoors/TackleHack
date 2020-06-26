using System;
using System.Collections.Generic;

namespace TackleHack.Models
{
    public partial class Account
    {
        public Account()
        {
            Membership = new HashSet<Membership>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Membership> Membership { get; set; }
    }
}
