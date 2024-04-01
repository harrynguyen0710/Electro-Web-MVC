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
    public class VeGiamGiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeGiamGiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/VeGiamGia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VEGIAMGIA.Include(v => v.TyLeGiam);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/VeGiamGia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veGiamGia = await _context.VEGIAMGIA
                .Include(v => v.TyLeGiam)
                .FirstOrDefaultAsync(m => m.MaVeGiamGia == id);
            if (veGiamGia == null)
            {
                return NotFound();
            }

            return View(veGiamGia);
        }

        // GET: Admin/VeGiamGia/Create
        public IActionResult Create()
        {
            ViewData["MaTyLeGiam"] = new SelectList(_context.TYLEGIAM, "MaTyLeGiam", "MoTaTyLeGiam");
            return View();
        }

        // POST: Admin/VeGiamGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaVeGiamGia,Code,NgayThietLap,SoLuongToiDaSuDung,MaTyLeGiam")] VeGiamGia veGiamGia)
        {

                _context.Add(veGiamGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


        }

        // GET: Admin/VeGiamGia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veGiamGia = await _context.VEGIAMGIA.FindAsync(id);
            if (veGiamGia == null)
            {
                return NotFound();
            }
            ViewData["MaTyLeGiam"] = new SelectList(_context.TYLEGIAM, "MaTyLeGiam", "MoTaTyLeGiam", veGiamGia.MaTyLeGiam);
            return View(veGiamGia);
        }

        // POST: Admin/VeGiamGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaVeGiamGia,Code,NgayThietLap,SoLuongToiDaSuDung,MaTyLeGiam")] VeGiamGia veGiamGia)
        {
            if (id != veGiamGia.MaVeGiamGia)
            {
                return NotFound();
            }

            try
            {
                _context.Update(veGiamGia);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeGiamGiaExists(veGiamGia.MaVeGiamGia))
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
        // GET: Admin/VeGiamGia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veGiamGia = await _context.VEGIAMGIA
                .Include(v => v.TyLeGiam)
                .FirstOrDefaultAsync(m => m.MaVeGiamGia == id);
            if (veGiamGia == null)
            {
                return NotFound();
            }

            return View(veGiamGia);
        }

        // POST: Admin/VeGiamGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veGiamGia = await _context.VEGIAMGIA.FindAsync(id);
            if (veGiamGia != null)
            {
                _context.VEGIAMGIA.Remove(veGiamGia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeGiamGiaExists(int id)
        {
            return _context.VEGIAMGIA.Any(e => e.MaVeGiamGia == id);
        }
    }
}
