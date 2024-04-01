using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class TyLeGiamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TyLeGiamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TyLeGiam
        public async Task<IActionResult> Index()
        {
            return View(await _context.TYLEGIAM.ToListAsync());
        }

        // GET: Admin/TyLeGiam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tyLeGiam = await _context.TYLEGIAM
                .FirstOrDefaultAsync(m => m.MaTyLeGiam == id);
            if (tyLeGiam == null)
            {
                return NotFound();
            }

            return View(tyLeGiam);
        }

        // GET: Admin/TyLeGiam/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTyLeGiam,MoTaTyLeGiam,PhanTramGiam")] TyLeGiam tyLeGiam)
        {
            _context.Add(tyLeGiam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/TyLeGiam/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tyLeGiam = await _context.TYLEGIAM.FindAsync(id);
            if (tyLeGiam == null)
            {
                return NotFound();
            }
            return View(tyLeGiam);
        }

        // POST: Admin/TyLeGiam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTyLeGiam,MoTaTyLeGiam,PhanTramGiam")] TyLeGiam tyLeGiam)
        {
            if (id != tyLeGiam.MaTyLeGiam)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(tyLeGiam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TyLeGiamExists(tyLeGiam.MaTyLeGiam))
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

        // GET: Admin/TyLeGiam/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tyLeGiam = await _context.TYLEGIAM
                .FirstOrDefaultAsync(m => m.MaTyLeGiam == id);
            if (tyLeGiam == null)
            {
                return NotFound();
            }

            return View(tyLeGiam);
        }

        // POST: Admin/TyLeGiam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tyLeGiam = await _context.TYLEGIAM.FindAsync(id);
            if (tyLeGiam != null)
            {
                _context.TYLEGIAM.Remove(tyLeGiam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TyLeGiamExists(int id)
        {
            return _context.TYLEGIAM.Any(e => e.MaTyLeGiam == id);
        }
    }
}
