using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RamController : Controller
    {
        private readonly IGenericRepository<Ram> _genericRepository;
        public RamController(IGenericRepository<Ram> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IActionResult> Index()
        {
            var ram = await _genericRepository.GetAllAsync();
            return View(ram);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _genericRepository.GetByIdAsync(id);

            if (ram == null)
            {
                return NotFound();
            }

            return View(ram);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaRam,TenRam")] Ram ram)
        {
            await _genericRepository.AddAsync(ram);
            return RedirectToAction("Index", "Ram");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _genericRepository.GetByIdAsync(id);
            if (ram == null)
            {
                return NotFound();
            }
            return View(ram);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaRam,TenRam")] Ram ram)
        {
            if (id != ram.MaRam)
            {
                return NotFound();
            }
            await _genericRepository.Update(ram);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _genericRepository.GetByIdAsync(id);
            if (ram == null)
            {
                return NotFound();
            }

            return View(ram);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ram = await _genericRepository.GetByIdAsync(id);
            if (ram != null)
            {
                await _genericRepository.Delete(ram);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
