using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;
using DACS.Repository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TrangThaiThanhToanController : Controller
    {
        private readonly ToolsRepository<TrangThaiThanhToan> _trangThaiThanhToanRepository;
        public TrangThaiThanhToanController(ToolsRepository<TrangThaiThanhToan> trangThaiThanhToanRepository)
        {
            _trangThaiThanhToanRepository = trangThaiThanhToanRepository;
        }
        public async Task<IActionResult> Index()
        {
            var trangThai = await _trangThaiThanhToanRepository.GetAllAsync();
            return View(trangThai);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTrangThaiThanhToan,TenTrangThaiThanhToan")] TrangThaiThanhToan trangThaiThanhToan)
        {   
            await _trangThaiThanhToanRepository.AddAsync(trangThaiThanhToan);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThaiThanhToan = await _trangThaiThanhToanRepository.GetByIdAsync(id);
            if (trangThaiThanhToan == null)
            {
                return NotFound();
            }
            return View(trangThaiThanhToan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTrangThaiThanhToan,TenTrangThaiThanhToan")] TrangThaiThanhToan trangThaiThanhToan)
        {
            if (id != trangThaiThanhToan.MaTrangThaiThanhToan)
            {
                return NotFound();
            }
            await _trangThaiThanhToanRepository.Update(trangThaiThanhToan);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trangThaiThanhToan = await _trangThaiThanhToanRepository.GetByIdAsync(id);
            if (trangThaiThanhToan == null)
            {
                return NotFound();
            }

            return View(trangThaiThanhToan);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trangThaiThanhToan = await _trangThaiThanhToanRepository.GetByIdAsync(id);
            if (trangThaiThanhToan != null)
            {
                await _trangThaiThanhToanRepository.Delete(trangThaiThanhToan);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
