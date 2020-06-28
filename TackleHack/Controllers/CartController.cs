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
    public class CartController : Controller
    {
        // GET: Cart
        public async Task<IActionResult> Index()
        {
            using (var context = new TackleHackSQLContext())
            {
                var tackleHackSQLContext = context.Cart.Include(c => c.Product).Where(x => x.UserName == User.Identity.Name);
                return View(await tackleHackSQLContext.ToListAsync());
            }
        }

        // POST: Cart/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            using (var context = new TackleHackSQLContext())
            {
                var cart = await context.Cart.FindAsync(id);
                context.Remove(cart);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> CartItemBadge()
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return NotFound();

            using (var context = new TackleHackSQLContext())
            {
                var cartItems = await context.Cart.Where(x => x.UserName == userName).ToListAsync();
                return Ok(cartItems.Count());
            }
        }
    }
}
