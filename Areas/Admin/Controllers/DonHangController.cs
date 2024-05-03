using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DACS.Data;
using DACS.Models;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDonHang _donHangRepository;

        public DonHangController(ApplicationDbContext context, IDonHang donHangRepository)
        {
            _context = context;
            _donHangRepository = donHangRepository;
        }
        public async Task<IActionResult> Index()
        {
            var donHang = await _donHangRepository.GetAllAsync();
            return View(donHang);
        }

        public async Task<IActionResult> Details(int? maDonHang)
        {
            if (maDonHang == null)
            {
                return NotFound();
            }

            var donHang = await _donHangRepository.GetByIdAsync(maDonHang);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        public async Task<IActionResult> Edit(int? maDonHang)
        {
            if (maDonHang == null)
            {
                return NotFound();
            }
            var donHang = await _donHangRepository.GetByIdAsync(maDonHang);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewBag.TrangThaiDonHangList = new SelectList(_context.TRANGTHAIDONHANG, "MaTrangThaiDonHang", "TenTrangThaiDonHang", donHang.MaTrangThaiDonHang);
            ViewBag.TrangThaiThanhToanList = new SelectList(_context.TRANGTHAITHANHTOAN, "MaTrangThaiThanhToan", "TenTrangThaiThanhToan", donHang.MaTrangThaiThanhToan);
            return View(donHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DonHang donHang)
        {
            if (donHang == null)
            {
                return NotFound();
            }
            await _donHangRepository.Update(donHang);
            return View(donHang);
        }

       
    }
}
