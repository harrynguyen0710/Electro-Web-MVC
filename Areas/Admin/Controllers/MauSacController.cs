using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MauSacController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MauSacController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.MAUSAC.ToListAsync());
        }
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMauSac,TenMau")] MauSac mauSac)
        {
            _context.Add(mauSac);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "MauSac");
        }
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


        }
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
