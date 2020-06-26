using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TackleHack.Models;

namespace TackleHack.ViewModels
{
    public class ShopItem
    {
        public Product Product { get; set; }
        public String YouTubeLink { get; set; }
        public String YouTubeEmbeddedLink { get; set; }
        public List<ReviewWithUser> Reviews { get; set; }
        public String Review { get; set; }
        public String UserName { get; set; }
    }
}
