using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;
using DACS.IRepository;
using System.Runtime.Intrinsics.Arm;

namespace DACS.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class LoaiSanPhamController : Controller
    {
        private readonly IToolsRepository<LoaiSanPham> _genericRepository;
        public LoaiSanPhamController(IToolsRepository<LoaiSanPham> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IActionResult> Index()
        {
            var dongSanPham = await _genericRepository.GetAllAsync();
            return View(dongSanPham);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _genericRepository.GetByIdAsync(id);

            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiSanPham,TenLoaiSanPham")] LoaiSanPham loaiSanPham)
        {
            await _genericRepository.AddAsync(loaiSanPham);
            return View(loaiSanPham);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _genericRepository.GetByIdAsync(id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            return View(loaiSanPham);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiSanPham,TenLoaiSanPham")] LoaiSanPham loaiSanPham)
        {
            if (id != loaiSanPham.MaLoaiSanPham)
            {
                return NotFound();
            }
            await _genericRepository.Update(loaiSanPham);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _genericRepository.GetByIdAsync(id);

            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSanPham = await _genericRepository.GetByIdAsync(id);
            if (loaiSanPham != null)
            {
                await _genericRepository.Delete(loaiSanPham);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
