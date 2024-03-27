using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebDT.Data;
using WebDT.Models;
using WebDT.ViewModel;

namespace WebDT.Controllers
{
    public class KiemTraDonHangController : Controller
    {
        private readonly ApplicationDbContext _dataContext;

        public KiemTraDonHangController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ThongTinDonHang(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập số điện thoại.";
                return View("Index");
            }

            var donHang = _dataContext.DonHang.Where(d => d.SoDienThoai == phoneNumber).ToList();

            if (donHang.Any()) // Check if there are any orders found
            {
                var viewModel = new DonHangViewModel
                {
                    DonHangList = donHang,
                    _context = _dataContext
                };

                return View("ThongTinDonHang", viewModel);
            }
            else
            {
                ViewBag.ErrorMessage = "Số điện thoại không khả dụng.";
                return View("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int MaDonHang)
        {
            var maTrangThaiDonHang = await _dataContext.DonHang.Where(x => x.MaDonHang == MaDonHang).Select(m => m.MaTrangThaiDonHang).FirstOrDefaultAsync();
            var maTrangThaiThanhToan = await _dataContext.DonHang.Where(x => x.MaDonHang == MaDonHang).Select(m => m.MaTrangThaiThanhToan).FirstOrDefaultAsync();


            var donHang = await _dataContext.DonHang.FirstOrDefaultAsync(x => x.MaDonHang == MaDonHang);
            var chiTietDonHang = await _dataContext.ChiTietDonHangSanPham.Where(x => x.MaDonHang == MaDonHang).ToListAsync();
            var trangThaiDonHang = await _dataContext.TrangThaiDonHang.FirstOrDefaultAsync(x => x.MaTrangThaiDonHang == maTrangThaiDonHang);
            var trangThaiThanhToan = await _dataContext.TrangThaiThanhToan.FirstOrDefaultAsync(x => x.MaTrangThaiThanhToan == maTrangThaiThanhToan);
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

            return View(viewModel);
        }

    }
}
