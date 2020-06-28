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
            var shopItem = await GetShopItem(id);
            if (shopItem == null)
                return NotFound();

            return View(shopItem);
        }

        public async Task<ShopItem> GetShopItem(int? id)
        {
            if (id == null)
                return null;

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
                    return null;

                if (shopItem.Product.Media.Count() > 0)
                {
                    shopItem.YouTubeLink = shopItem.Product.Media.First().Link;
                    shopItem.YouTubeEmbeddedLink = "https://www.youtube.com/embed/"
                        + shopItem.YouTubeLink.Substring(shopItem.YouTubeLink.LastIndexOf('/') + 1);
                }

                if (shopItem.Product.ProductReview.Count() > 0)
                {
                    shopItem.PercentReview = Convert.ToInt32(GetPercentReview(shopItem.Product.ProductReview));
                    shopItem.AverageReview = Convert.ToInt32(GetAverageReview(shopItem.Product.ProductReview));
                }

                return shopItem;
            }
        }

        private double GetPercentReview(ICollection<ProductReview> reviews)
        {
            var denominator = reviews.Count() * 5;
            var totalScore = 0;
            foreach (var review in reviews)
            {
                totalScore += review.Review.Rating;
            }
            var percentage = (double)totalScore / (double)denominator;
            return (percentage * 100);
        }

        private double GetAverageReview(ICollection<ProductReview> reviews)
        {
            var denominator = reviews.Count();
            var totalScore = 0;
            foreach (var review in reviews)
            {
                totalScore += review.Review.Rating;
            }
            var average = (double)totalScore / (double)denominator;
            return average;
        }

        [HttpPost]
        public async Task<IActionResult> Review(String reviewText, String userName, int productId, int productRating)
        {
            using (var context = new TackleHackSQLContext())
            {
                var review = new Review()
                {
                    Text = reviewText,
                    UserName = userName,
                    DateTime = DateTime.Now,
                    Rating = productRating
                };
                context.Add(review);
                await context.SaveChangesAsync();

                var productReview = new ProductReview()
                {
                    ReviewId = review.Id,
                    ProductId = productId
                };
                context.Add(productReview);
                await context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = productId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(String userName, int productId)
        {
            using (var context = new TackleHackSQLContext())
            {
                var cart = new Cart()
                {
                    UserName = userName,
                    DateTime = DateTime.Now,
                    ProductId = productId,
                    Quantity = 1,
                    Status = 0
                };
                context.Add(cart);
                await context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = productId });
            }
        }
    }
}
