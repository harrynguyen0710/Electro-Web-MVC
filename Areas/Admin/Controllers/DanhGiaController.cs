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
    public class DanhGiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DanhGiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/DanhGia
        public async Task<IActionResult> Index()
        {
            return View(await _context.DANHGIA.ToListAsync());
        }

        // GET: Admin/DanhGia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DANHGIA
                .FirstOrDefaultAsync(m => m.MaDanhGia == id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return View(danhGia);
        }

        // GET: Admin/DanhGia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDanhGia,MoTaDanhGia,DiemDanhGia")] DanhGia danhGia)
        {

                _context.Add(danhGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/DanhGia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DANHGIA.FindAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }
            return View(danhGia);
        }

        // POST: Admin/DanhGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDanhGia,MoTaDanhGia,DiemDanhGia")] DanhGia danhGia)
        {
            if (id != danhGia.MaDanhGia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhGiaExists(danhGia.MaDanhGia))
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
            return View(danhGia);
        }

        // GET: Admin/DanhGia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _context.DANHGIA
                .FirstOrDefaultAsync(m => m.MaDanhGia == id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return View(danhGia);
        }

        // POST: Admin/DanhGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhGia = await _context.DANHGIA.FindAsync(id);
            if (danhGia != null)
            {
                _context.DANHGIA.Remove(danhGia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhGiaExists(int id)
        {
            return _context.DANHGIA.Any(e => e.MaDanhGia == id);
        }
    }
}
