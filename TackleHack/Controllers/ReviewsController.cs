using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TackleHack.Models;

namespace TackleHack.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void LeaveReview(String reviewText, String userName, int productId)
        {
            using (var context = new TackleHackSQLContext())
            {
                var review = new Review()
                { 
                    Text = reviewText,
                    UserName = userName,
                    DateTime = new DateTime(),
                };
                context.Review.Add(review);
                context.SaveChanges();

                var productReview = new ProductReview()
                {
                    ReviewId = review.Id,
                    ProductId = productId
                };
                context.ProductReview.Add(productReview);
                context.SaveChanges();
            }            
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var context = new TackleHackSQLContext())
            {
                var review = await context.Review.FindAsync(id);
                context.Review.Remove(review);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
