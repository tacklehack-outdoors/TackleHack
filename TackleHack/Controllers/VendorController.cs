using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TackleHack.Models;
using TackleHack.Shared;

namespace TackleHack.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public async Task<IActionResult> Index()
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

                return View(vendors);
            }
        }

        // GET: Vendor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var context = new TackleHackSQLContext())
            {
                var vendor = await context.Vendor
                .FirstOrDefaultAsync(m => m.Id == id);
                if (vendor == null)
                {
                    return NotFound();
                }

                return View(vendor);
            }
        }

        // GET: Vendor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Email")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                using (var context = new TackleHackSQLContext())
                {
                    context.Add(vendor);
                    await context.SaveChangesAsync();

                    var membership = new Membership()
                    {
                        AccountId = 1,
                        VendorId = vendor.Id,
                        UserName = User.Identity.Name
                    };
                    context.Add(membership);
                    await context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        // GET: Vendor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var context = new TackleHackSQLContext())
            {
                var vendor = await context.Vendor.FindAsync(id);
                if (vendor == null)
                {
                    return NotFound();
                }
                return View(vendor);
            }
        }

        // POST: Vendor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Email")] Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new TackleHackSQLContext())
                    {
                        context.Update(vendor);
                        await context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.Id))
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
            return View(vendor);
        }

        // GET: Vendor/ImportProducts/5
        public async Task<IActionResult> ImportProducts(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var context = new TackleHackSQLContext())
            {
                var vendor = await context.Vendor
                .FirstOrDefaultAsync(m => m.Id == id);
                if (vendor == null)
                {
                    return NotFound();
                }

                return View(vendor);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportProducts(List<IFormFile> files, [Bind("Id,Name,Phone,Email")] Vendor vendor)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            foreach (var filePath in filePaths)
            {
                VendorOnboardingHelper.ProcessVendorProducts(vendor.Id, filePath);
            }

            return View(vendor);
        }

        // GET: Vendor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var context = new TackleHackSQLContext())
            {
                var vendor = await context.Vendor
                .FirstOrDefaultAsync(m => m.Id == id);
                if (vendor == null)
                {
                    return NotFound();
                }

                return View(vendor);
            }
        }

        // POST: Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var context = new TackleHackSQLContext())
            {
                var vendor = await context.Vendor.FindAsync(id);
                context.Vendor.Remove(vendor);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            using (var context = new TackleHackSQLContext())
            {
                return context.Vendor.Any(e => e.Id == id);
            }
        }
    }
}
