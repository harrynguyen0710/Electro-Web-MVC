using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChuDeController : Controller
    {
        private readonly IToolsRepository<ChuDe> _repository;
        public ChuDeController(IToolsRepository<ChuDe> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var chuDe = await _repository.GetAllAsync();
            return View(chuDe);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _repository.GetByIdAsync(id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChuDe,TenChuDe,Mota")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(chuDe);
                return RedirectToAction(nameof(Index));
            }
            return View(chuDe);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _repository.GetByIdAsync(id);
            if (chuDe == null)
            {
                return NotFound();
            }
            return View(chuDe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaChuDe,TenChuDe,Mota")] ChuDe chuDe)
        {
            if (id != chuDe.MaChuDe)
            {
                return NotFound();
            }
            await _repository.Update(chuDe);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _repository.GetByIdAsync(id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chuDe = await _repository.GetByIdAsync(id);
            if (chuDe != null)
            {
                await _repository.Delete(chuDe);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}