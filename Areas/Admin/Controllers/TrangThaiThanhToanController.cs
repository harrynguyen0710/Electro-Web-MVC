using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrangThaiThanhToanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrangThaiThanhToanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ram
        public async Task<IActionResult> Index()
        {
            return View(await _context.TRANGTHAITHANHTOAN.ToListAsync());
        }

    

        // GET: Ram/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ram/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTrangThaiDonHang, TenTrangThaiDonHang")] TrangThaiThanhToan trangThai)
        {
            _context.Add(trangThai);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "TrangThaiThanhToan");

        }

        // GET: Ram/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TRANGTHAITHANHTOAN.FindAsync(id);
            if (trangThai == null)
            {
                return NotFound();
            }
            return View(trangThai);
        }

        // POST: Ram/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTrangThaiDonHang, TenTrangThaiDonHang")] TrangThaiThanhToan trangThai)
        {
            if (id != trangThai.MaTrangThaiThanhToan)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(trangThai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangThaiThanhToanExists(trangThai.MaTrangThaiThanhToan))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(trangThai);
        }

        // GET: Ram/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TRANGTHAITHANHTOAN
                .FirstOrDefaultAsync(m => m.MaTrangThaiThanhToan == id);
            if (trangThai == null)
            {
                return NotFound();
            }

            return View(trangThai);
        }

        // POST: Ram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trangThai = await _context.TRANGTHAITHANHTOAN.FindAsync(id);
            if (trangThai != null)
            {
                _context.TRANGTHAITHANHTOAN.Remove(trangThai);
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
