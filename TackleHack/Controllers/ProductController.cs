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
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<IActionResult> Index()
        {
            using (var context = new TackleHackSQLContext())
            {
                var vendorIds = await context.Membership
                    .Where(x => x.UserName == User.Identity.Name)
                    .Select(x => x.VendorId)
                    .ToListAsync();
                var products = await context.Product
                    .Where(x => vendorIds.Contains(x.VendorId))
                    .Include(x => x.Vendor)
                    .ToListAsync();

                return View(products);
            }
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var context = new TackleHackSQLContext())
            {
                var product = await context.Product
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }

        // GET: Product/Create
        public async Task<IActionResult> CreateAsync()
        {
            using (var context = new TackleHackSQLContext())
            {
                var vendorIds = await context.Membership
                    .Where(x => x.UserName == User.Identity.Name)
                    .Select(x => x.VendorId)
                    .ToListAsync();
                var vendors = await context.Vendor
                    .Where(x => vendorIds.Contains(x.Id))
                    .ToListAsync();
                ViewData["VendorId"] = new SelectList(vendors, "Id", "Name");
                return View();
            }
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemNumber,BrandName,ProductName,Description,Sku,Msrp,VendorId")] Product product)
        {
            using (var context = new TackleHackSQLContext())
            {
                if (ModelState.IsValid)
                {
                    context.Add(product);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["VendorId"] = new SelectList(await context.Vendor.ToListAsync(), "Id", "Name", product.VendorId);
                return View(product);
            }
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var context = new TackleHackSQLContext())
            {
                var product = await context.Product.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                var vendorIds = await context.Membership
                    .Where(x => x.UserName == User.Identity.Name)
                    .Select(x => x.VendorId)
                    .ToListAsync();
                var vendors = await context.Vendor
                    .Where(x => vendorIds.Contains(x.Id))
                    .ToListAsync();
                ViewData["VendorId"] = new SelectList(vendors, "Id", "Name", product.VendorId);
                return View(product);
            }
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemNumber,BrandName,ProductName,Description,Sku,Msrp,VendorId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            using (var context = new TackleHackSQLContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        context.Update(product);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }

                var vendorIds = await context.Membership
                    .Where(x => x.UserName == User.Identity.Name)
                    .Select(x => x.VendorId)
                    .ToListAsync();
                var vendors = await context.Vendor
                    .Where(x => vendorIds.Contains(x.Id))
                    .ToListAsync();
                ViewData["VendorId"] = new SelectList(vendors, "Id", "Name", product.VendorId);
                return View(product);
            }
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var context = new TackleHackSQLContext())
            {
                var product = await context.Product
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var context = new TackleHackSQLContext())
            {
                var productFeatures = context.ProductFeature.Where(x => x.ProductId == id);
                context.ProductFeature.RemoveRange(productFeatures);
                await context.SaveChangesAsync();

                var productReviews = context.ProductReview.Where(x => x.ProductId == id);
                context.ProductReview.RemoveRange(productReviews);
                await context.SaveChangesAsync();

                var cartItems = context.Cart.Where(x => x.ProductId == id);
                context.Cart.RemoveRange(cartItems);
                await context.SaveChangesAsync();

                var media = context.Media.Where(x => x.ProductId == id);
                context.Media.RemoveRange(media);
                await context.SaveChangesAsync();

                var product = await context.Product.FindAsync(id);
                context.Product.Remove(product);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ProductExists(int id)
        {
            using (var context = new TackleHackSQLContext())
            {
                return context.Product.Any(e => e.Id == id);
            }
        }
    }
}
