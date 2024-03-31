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
    public class TrangThaiThanhToanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrangThaiThanhToanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TrangThaiThanhToan
        public async Task<IActionResult> Index()
        {
            return View(await _context.TRANGTHAITHANHTOAN.ToListAsync());
        }

        // GET: Admin/TrangThaiThanhToan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThaiThanhToan = await _context.TRANGTHAITHANHTOAN
                .FirstOrDefaultAsync(m => m.MaTrangThaiThanhToan == id);
            if (trangThaiThanhToan == null)
            {
                return NotFound();
            }

            return View(trangThaiThanhToan);
        }

        // GET: Admin/TrangThaiThanhToan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TrangThaiThanhToan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTrangThaiThanhToan,TenTrangThaiThanhToan")] TrangThaiThanhToan trangThaiThanhToan)
        {

                _context.Add(trangThaiThanhToan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/TrangThaiThanhToan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThaiThanhToan = await _context.TRANGTHAITHANHTOAN.FindAsync(id);
            if (trangThaiThanhToan == null)
            {
                return NotFound();
            }
            return View(trangThaiThanhToan);
        }

        // POST: Admin/TrangThaiThanhToan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTrangThaiThanhToan,TenTrangThaiThanhToan")] TrangThaiThanhToan trangThaiThanhToan)
        {
            if (id != trangThaiThanhToan.MaTrangThaiThanhToan)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(trangThaiThanhToan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangThaiThanhToanExists(trangThaiThanhToan.MaTrangThaiThanhToan))
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

        // GET: Admin/TrangThaiThanhToan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThaiThanhToan = await _context.TRANGTHAITHANHTOAN
                .FirstOrDefaultAsync(m => m.MaTrangThaiThanhToan == id);
            if (trangThaiThanhToan == null)
            {
                return NotFound();
            }

            return View(trangThaiThanhToan);
        }

        // POST: Admin/TrangThaiThanhToan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trangThaiThanhToan = await _context.TRANGTHAITHANHTOAN.FindAsync(id);
            if (trangThaiThanhToan != null)
            {
                _context.TRANGTHAITHANHTOAN.Remove(trangThaiThanhToan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangThaiThanhToanExists(int id)
        {
            return _context.TRANGTHAITHANHTOAN.Any(e => e.MaTrangThaiThanhToan == id);
        }
    }
}
