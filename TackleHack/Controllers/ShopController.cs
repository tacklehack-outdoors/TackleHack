using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TackleHack.Data;
using TackleHack.Models;
using TackleHack.ViewModels;

namespace TackleHack.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public async Task<IActionResult> Index()
        {
            using (var context = new TackleHackSQLContext())
            {
                var tackleHackSQLContext = context.Product.Include(p => p.Vendor);
                return View(await tackleHackSQLContext.ToListAsync());
            }
        }

        // GET: Shop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var shopItem = new ShopItem();
            using (var context = new TackleHackSQLContext())
            {
                shopItem.Product = await context.Product
                .Include(p => p.Vendor)
                .Include(p => p.Media)
                .Include(p => p.ProductFeature)
                .ThenInclude(p => p.Feature)
                .Include(p => p.ProductReview)
                .ThenInclude(p => p.Review)
                .FirstOrDefaultAsync(m => m.Id == id);

                if (shopItem.Product == null)
                    return NotFound();

                if (shopItem.Product.Media.Count() > 0)
                {
                    shopItem.YouTubeLink = shopItem.Product.Media.First().Link;
                    shopItem.YouTubeEmbeddedLink = "https://www.youtube.com/embed/" 
                        + shopItem.YouTubeLink.Substring(shopItem.YouTubeLink.LastIndexOf('/') + 1);
                }

                shopItem.Reviews = new List<ReviewWithUser>();
                foreach (var review in shopItem.Product.ProductReview)
                {
                    var tempReview = new ReviewWithUser();
                    tempReview.Review = review.Review;

                }

                return View(shopItem);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Review(String reviewText, String userName, int productId)
        {
            using (var context = new TackleHackSQLContext())
            {
                var review = new Review()
                {
                    Text = reviewText,
                    UserName = userName,
                    DateTime = DateTime.Now
                };
                context.Review.Add(review);
                await context.SaveChangesAsync();

                var productReview = new ProductReview()
                {
                    ReviewId = review.Id,
                    ProductId = productId
                };
                context.ProductReview.Add(productReview);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Details));
            }
        }
    }
}
