using Microsoft.AspNetCore.Mvc;
using DACS.Data;
using DACS.Models;
using DACS.IRepository;


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
