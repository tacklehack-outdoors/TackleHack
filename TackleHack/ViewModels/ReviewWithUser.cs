using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TackleHack.Models;

namespace TackleHack.ViewModels
{
    public class ReviewWithUser
    {
        public Review Review { get; set; }
        public string UserName { get; set; }
    }
}
