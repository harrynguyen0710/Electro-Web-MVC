using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDT.Data;
using WebDT.Models;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MauSacController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MauSacController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MauSac
        public async Task<IActionResult> Index()
        {
            return View(await _context.MAUSAC.ToListAsync());
        }

        // GET: MauSac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauSac = await _context.MAUSAC
                .FirstOrDefaultAsync(m => m.MaMauSac == id);
            if (mauSac == null)
            {
                return NotFound();
            }

            return View(mauSac);
        }

        // GET: MauSac/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MauSac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMauSac,TenMau")] MauSac mauSac)
        {
            _context.Add(mauSac);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "MauSac");
        }

        // GET: MauSac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauSac = await _context.MAUSAC.FindAsync(id);
            if (mauSac == null)
            {
                return NotFound();
            }
            return View(mauSac);
        }

        // POST: MauSac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaMauSac,TenMau")] MauSac mauSac)
        {
            if (id != mauSac.MaMauSac)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(mauSac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MauSacExists(mauSac.MaMauSac))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            return View(mauSac);
        }

        // GET: MauSac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauSac = await _context.MAUSAC
                .FirstOrDefaultAsync(m => m.MaMauSac == id);
            if (mauSac == null)
            {
                return NotFound();
            }

            return View(mauSac);
        }

        // POST: MauSac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mauSac = await _context.MAUSAC.FindAsync(id);
            if (mauSac != null)
            {
                _context.MAUSAC.Remove(mauSac);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MauSacExists(int id)
        {
            return _context.MAUSAC.Any(e => e.MaMauSac == id);
        }
    }
}
