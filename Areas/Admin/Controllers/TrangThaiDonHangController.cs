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

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TrangThaiDonHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrangThaiDonHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ram
        public async Task<IActionResult> Index()
        {
            return View(await _context.TRANGTHAIDONHANG.ToListAsync());
        }



        // GET: Ram/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTrangThaiDonHang, TenTrangThaiDonHang")] TrangThaiDonHang trangThai)
        {
            _context.Add(trangThai);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "TrangThaiDonHang");

        }

        // GET: Ram/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TRANGTHAIDONHANG.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("MaTrangThaiDonHang, TenTrangThaiDonHang")] TrangThaiDonHang trangThai)
        {
            if (id != trangThai.MaTrangThaiDonHang)
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
                    if (!TrangThaiThanhToanExists(trangThai.MaTrangThaiDonHang))
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

        // GET: Ram/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _context.TRANGTHAIDONHANG
                .FirstOrDefaultAsync(m => m.MaTrangThaiDonHang == id);
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
            var trangThai = await _context.TRANGTHAIDONHANG.FindAsync(id);
            if (trangThai != null)
            {
                _context.TRANGTHAIDONHANG.Remove(trangThai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangThaiThanhToanExists(int id)
        {
            return _context.TRANGTHAIDONHANG.Any(e => e.MaTrangThaiDonHang == id);
        }
    }
}
