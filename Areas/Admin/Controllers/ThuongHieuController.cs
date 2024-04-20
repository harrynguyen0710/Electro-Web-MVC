using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ThuongHieuController : Controller
    {
        private readonly IGenericRepository<ThuongHieu> _genericRepository;
        public ThuongHieuController(IGenericRepository<ThuongHieu> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IActionResult> Index()
        {
            var thuongHieu = await _genericRepository.GetAllAsync();
            return View(thuongHieu);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuongHieu = await _genericRepository.GetByIdAsync(id);

            if (thuongHieu == null)
            {
                return NotFound();
            }

            return View(thuongHieu);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThuongHieu,TenThuongHieu")] ThuongHieu thuongHieu)
        {
            await _genericRepository.AddAsync(thuongHieu);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuongHieu = await _genericRepository.GetByIdAsync(id);
            if (thuongHieu == null)
            {
                return NotFound();
            }
            return View(thuongHieu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThuongHieu,TenThuongHieu")] ThuongHieu thuongHieu)
        {
            if (id != thuongHieu.MaThuongHieu)
            {
                return NotFound();
            }
            await _genericRepository.Update(thuongHieu);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var thuongHieu = await _genericRepository.GetByIdAsync(id);

            if (thuongHieu == null)
            {
                return NotFound();
            }

            return View(thuongHieu);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thuongHieu = await _genericRepository.GetByIdAsync(id);
            if (thuongHieu != null)
            {
                await _genericRepository.Delete(thuongHieu);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
