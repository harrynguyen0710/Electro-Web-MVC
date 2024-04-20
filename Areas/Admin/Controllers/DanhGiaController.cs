using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DanhGiaController : Controller
    {
        private readonly IToolsRepository<DanhGia> _repository;
        public DanhGiaController(IToolsRepository<DanhGia> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _repository.GetByIdAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return View(danhGia);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDanhGia,MoTaDanhGia,DiemDanhGia")] DanhGia danhGia)
        {
            await _repository.AddAsync(danhGia);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _repository.GetByIdAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }
            return View(danhGia);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDanhGia,MoTaDanhGia,DiemDanhGia")] DanhGia danhGia)
        {
            if (id != danhGia.MaDanhGia)
            {
                return NotFound();
            }
            await _repository.Update(danhGia);
            return View(danhGia);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhGia = await _repository.GetByIdAsync(id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return View(danhGia);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhGia = await _repository.GetByIdAsync(id);
            if (danhGia != null)
            {
                await _repository.Delete(danhGia);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
