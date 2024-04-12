using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonHangsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/DonHangs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DONHANG.Include(d => d.TrangThaiDonHang).Include(d => d.TrangThaiThanhToan).Include(d => d.VeGiamGia);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/DonHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DONHANG
                .Include(d => d.TrangThaiDonHang)
                .Include(d => d.TrangThaiThanhToan)
                .Include(d => d.VeGiamGia)
                .FirstOrDefaultAsync(m => m.MaDonHang == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: Admin/DonHangs/Create
        public IActionResult Create()
        {
            ViewData["MaTrangThaiDonHang"] = new SelectList(_context.TRANGTHAIDONHANG, "MaTrangThaiDonHang", "MaTrangThaiDonHang");
            ViewData["MaTrangThaiThanhToan"] = new SelectList(_context.TRANGTHAITHANHTOAN, "MaTrangThaiThanhToan", "MaTrangThaiThanhToan");
            ViewData["MaVeGiamGia"] = new SelectList(_context.VEGIAMGIA, "MaVeGiamGia", "MaVeGiamGia");
            return View();
        }

        // POST: Admin/DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDonHang,NgayLapDonHang,TongGiaTriDonHang,MaTrangThaiThanhToan,MaTrangThaiDonHang,MaVeGiamGia,TenKhachHang,SoDienThoai,DiaChi,YeuCauKhac")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaTrangThaiDonHang"] = new SelectList(_context.TRANGTHAIDONHANG, "MaTrangThaiDonHang", "MaTrangThaiDonHang", donHang.MaTrangThaiDonHang);
            ViewData["MaTrangThaiThanhToan"] = new SelectList(_context.TRANGTHAITHANHTOAN, "MaTrangThaiThanhToan", "MaTrangThaiThanhToan", donHang.MaTrangThaiThanhToan);
            ViewData["MaVeGiamGia"] = new SelectList(_context.VEGIAMGIA, "MaVeGiamGia", "MaVeGiamGia", donHang.MaVeGiamGia);
            return View(donHang);
        }

        // GET: Admin/DonHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DONHANG.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewData["MaTrangThaiDonHang"] = new SelectList(_context.TRANGTHAIDONHANG, "MaTrangThaiDonHang", "MaTrangThaiDonHang", donHang.MaTrangThaiDonHang);
            ViewData["MaTrangThaiThanhToan"] = new SelectList(_context.TRANGTHAITHANHTOAN, "MaTrangThaiThanhToan", "MaTrangThaiThanhToan", donHang.MaTrangThaiThanhToan);
            ViewData["MaVeGiamGia"] = new SelectList(_context.VEGIAMGIA, "MaVeGiamGia", "MaVeGiamGia", donHang.MaVeGiamGia);
            return View(donHang);
        }

        // POST: Admin/DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDonHang,NgayLapDonHang,TongGiaTriDonHang,MaTrangThaiThanhToan,MaTrangThaiDonHang,MaVeGiamGia,TenKhachHang,SoDienThoai,DiaChi,YeuCauKhac")] DonHang donHang)
        {
            if (id != donHang.MaDonHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.MaDonHang))
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
            ViewData["MaTrangThaiDonHang"] = new SelectList(_context.TRANGTHAIDONHANG, "MaTrangThaiDonHang", "MaTrangThaiDonHang", donHang.MaTrangThaiDonHang);
            ViewData["MaTrangThaiThanhToan"] = new SelectList(_context.TRANGTHAITHANHTOAN, "MaTrangThaiThanhToan", "MaTrangThaiThanhToan", donHang.MaTrangThaiThanhToan);
            ViewData["MaVeGiamGia"] = new SelectList(_context.VEGIAMGIA, "MaVeGiamGia", "MaVeGiamGia", donHang.MaVeGiamGia);
            return View(donHang);
        }

        // GET: Admin/DonHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DONHANG
                .Include(d => d.TrangThaiDonHang)
                .Include(d => d.TrangThaiThanhToan)
                .Include(d => d.VeGiamGia)
                .FirstOrDefaultAsync(m => m.MaDonHang == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // POST: Admin/DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.DONHANG.FindAsync(id);
            if (donHang != null)
            {
                _context.DONHANG.Remove(donHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(int id)
        {
            return _context.DONHANG.Any(e => e.MaDonHang == id);
        }
    }
}
