
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DACS.Data;
using DACS.Models;
using DACS.ViewModel;

﻿using Microsoft.AspNetCore.Authorization;


namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DonHangController : Controller
    {        
        private readonly ApplicationDbContext _dataContext;

        public DonHangController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            /*            var donHang = await _dataContext.DONHANG.ToListAsync();
            */
            /*            var donHang = _dataContext.DONHANG.Include(d => d.TrangThaiDonHang).Include(d => d.TrangThaiThanhToan).Include(d => d.VeGiamGia);

                        if (donHang == null)
                        {
                            return NotFound();
                        }
                        var viewModel = new DonHangViewModel 
                        {
                            DonHangList = donHang,
                            _context = _dataContext
                        };
                        return View(viewModel);*/
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int MaDonHang)
        {
            var maTrangThaiDonHang = await _dataContext.DONHANG.Where(x => x.MaDonHang == MaDonHang).Select(m => m.MaTrangThaiDonHang).FirstOrDefaultAsync();
            var maTrangThaiThanhToan = await _dataContext.DONHANG.Where(x => x.MaDonHang == MaDonHang).Select(m => m.MaTrangThaiThanhToan).FirstOrDefaultAsync();
            var donHang = await _dataContext.DONHANG.FirstOrDefaultAsync(x => x.MaDonHang == MaDonHang);
            var chiTietDonHang = await _dataContext.CHITIETDONHANGSANPHAM.Where(x => x.MaDonHang == MaDonHang).ToListAsync();
            var trangThaiDonHang = await _dataContext.TRANGTHAIDONHANG.FirstOrDefaultAsync(x => x.MaTrangThaiDonHang == maTrangThaiDonHang);
            var trangThaiThanhToan = await _dataContext.TRANGTHAITHANHTOAN.FirstOrDefaultAsync(x => x.MaTrangThaiThanhToan == maTrangThaiThanhToan);
            var hinhAnh = await _dataContext.HINHANH.ToListAsync();
            if (donHang == null || chiTietDonHang == null || trangThaiDonHang == null || trangThaiThanhToan == null)
            {
                return NotFound();
            }

            List<SanPham> sanPham = new List<SanPham>();

            foreach (var ctDonHang in chiTietDonHang)
            {
                var sanPhamBan = await _dataContext.SANPHAM.FirstOrDefaultAsync(x => x.MaSanPham == ctDonHang.MaSanPham);
                sanPham = sanPham.Append(sanPhamBan).ToList();
            }

            if (sanPham == null)
            {
                return NotFound();
            }

            DonHangViewModel viewModel = new DonHangViewModel()
            {
                DonHang = donHang,
                ChiTietDonHangSanPhamList = chiTietDonHang,
                TrangThaiDonHang = trangThaiDonHang,
                TrangThaiThanhToan = trangThaiThanhToan,
                _context = _dataContext,
                SanPhamList = sanPham,
                HinhAnhList = hinhAnh

            };
            ViewBag.TrangThaiDonHangList = new SelectList(_dataContext.TRANGTHAIDONHANG, "MaTrangThaiDonHang", "TenTrangThaiDonHang");
            ViewBag.TrangThaiThanhToanList = new SelectList(_dataContext.TRANGTHAITHANHTOAN, "MaTrangThaiThanhToan", "TenTrangThaiThanhToan");
            return View(viewModel);
        }
    
        [HttpPost]
        public async Task<IActionResult> Edit(int MaDonHang, int MaTrangThaiDonHang, int MaTrangThaiThanhToan)
        {
            // Lấy đối tượng DonHang từ cơ sở dữ liệu
            var donHang = await _dataContext.DONHANG.FirstOrDefaultAsync(x => x.MaDonHang == MaDonHang);
            if (donHang == null)
            {
                return NotFound();
            }

            // Cập nhật trạng thái đơn hàng và trạng thái thanh toán
            donHang.MaTrangThaiDonHang = MaTrangThaiDonHang;
            donHang.MaTrangThaiThanhToan = MaTrangThaiThanhToan;

            // Lưu thay đổi vào cơ sở dữ liệu
            _dataContext.Entry(donHang).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            // Chuyển hướng về action Edit với MaDonHang tương ứng
            return RedirectToAction("Index", "DonHang");
        }


        [HttpGet]
        public async Task<IActionResult> Details(int MaDonHang)
        {
            var maTrangThaiDonHang = await _dataContext.DONHANG.Where(x => x.MaDonHang == MaDonHang).Select(m => m.MaTrangThaiDonHang).FirstOrDefaultAsync();
            var maTrangThaiThanhToan = await _dataContext.DONHANG.Where(x => x.MaDonHang == MaDonHang).Select(m => m.MaTrangThaiThanhToan).FirstOrDefaultAsync();


            var donHang = await _dataContext.DONHANG.FirstOrDefaultAsync(x => x.MaDonHang == MaDonHang);           
            var chiTietDonHang = await _dataContext.CHITIETDONHANGSANPHAM.Where(x => x.MaDonHang == MaDonHang).ToListAsync();
            var trangThaiDonHang = await _dataContext.TRANGTHAIDONHANG.FirstOrDefaultAsync(x => x.MaTrangThaiDonHang == maTrangThaiDonHang);
            var trangThaiThanhToan = await _dataContext.TRANGTHAITHANHTOAN.FirstOrDefaultAsync(x => x.MaTrangThaiThanhToan == maTrangThaiThanhToan);
            var hinhAnh = await _dataContext.HINHANH.ToListAsync();
            if (donHang == null || chiTietDonHang == null || trangThaiDonHang == null || trangThaiThanhToan == null)
            {
                return NotFound();
            }

            List<SanPham> sanPham = new List<SanPham>();
            
            foreach(var ctDonHang in chiTietDonHang)
            {
                var sanPhamBan = await _dataContext.SANPHAM.FirstOrDefaultAsync(x => x.MaSanPham == ctDonHang.MaSanPham);
                sanPham = sanPham.Append(sanPhamBan).ToList();
            }

            if (sanPham == null)
            {
                return NotFound();
            }

            DonHangViewModel viewModel = new DonHangViewModel()
            {
                DonHang = donHang,
                ChiTietDonHangSanPhamList = chiTietDonHang,
                TrangThaiDonHang = trangThaiDonHang,
                TrangThaiThanhToan = trangThaiThanhToan,
                _context = _dataContext,
                SanPhamList = sanPham,
                HinhAnhList = hinhAnh

            };

            return View(viewModel);
        }



    }
}
