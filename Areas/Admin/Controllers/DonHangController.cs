using Microsoft.AspNetCore.Mvc;
using DACS.Data;
using DACS.Models;
using DACS.IRepository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;


namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangController : Controller
    {
        private readonly IDonHang _donHangRepository;
        public DonHangController(IDonHang donHangRepository)
        {
            _donHangRepository = donHangRepository;
        }
        public async Task<IActionResult> Index(int? page , int? pageSize)
        {
            page = page ?? 1;
            pageSize = pageSize ?? 3;
            var donHang = _donHangRepository.GetAll();
            return View(donHang.ToPagedList(page.Value, pageSize.Value));
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
