using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.Repository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TrangThaiDonHangController : Controller
    {
        private readonly ToolsRepository<TrangThaiDonHang> _trangThaiDonHangRepository;
        public TrangThaiDonHangController(ToolsRepository<TrangThaiDonHang> trangThaiDonHangRepository)
        {
            _trangThaiDonHangRepository = trangThaiDonHangRepository;
        }
        public async Task<IActionResult> Index()
        {
            var trangThai = await _trangThaiDonHangRepository.GetAllAsync();
            return View(trangThai);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTrangThaiDonHang, TenTrangThaiDonHang")] TrangThaiDonHang trangThai)
        {
            await _trangThaiDonHangRepository.AddAsync(trangThai);
            return RedirectToAction("Index", "TrangThaiDonHang");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThai = await _trangThaiDonHangRepository.GetByIdAsync(id);
            if (trangThai == null)
            {
                return NotFound();
            }
            return View(trangThai);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTrangThaiDonHang, TenTrangThaiDonHang")] TrangThaiDonHang trangThai)
        {
            if (id != trangThai.MaTrangThaiDonHang)
            {
                return NotFound();
            }
            await _trangThaiDonHangRepository.Update(trangThai);
            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trangThai = await _trangThaiDonHangRepository.GetByIdAsync(id);
            if (trangThai == null)
            {
                return NotFound();
            }

            return View(trangThai);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trangThai = await _trangThaiDonHangRepository.GetByIdAsync(id);
            if (trangThai != null)
            {
                await _trangThaiDonHangRepository.Delete(trangThai);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
