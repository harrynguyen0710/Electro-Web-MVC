using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HinhAnhQuangCaoController : Controller
    {
        private readonly IToolsRepository<HinhAnhQuangCao> _genericRepository;
        private readonly IHinhAnh _repository;
        public HinhAnhQuangCaoController(IToolsRepository<HinhAnhQuangCao> genericRepository, IHinhAnh repository)
        {
            _genericRepository = genericRepository;
            _repository = repository;
        }

       /* public async Task<IActionResult> Index()
        {
            var anh = await _context.HINHANHQUANGCAO.ToListAsync();
            return View(anh);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HinhAnhQuangCao anh)
        {
            string uniqueFileName = GetProfilePhotoFileName(anh);
            anh.FileAnh = uniqueFileName;
            await _context.HINHANHQUANGCAO.AddAsync(anh);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "HinhAnhQuangCao");
        }

        public async Task<IActionResult> Delete(int maHinhAnh)
        {
            var anh = await _context.HINHANHQUANGCAO.Where(t => t.MaAnhQuangCao == maHinhAnh).FirstOrDefaultAsync();
            return View(anh);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(HinhAnhQuangCao anh)
        {
            _context.HINHANHQUANGCAO.Remove(anh);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "HinhAnhQuangCao");
        }

        private string GetProfilePhotoFileName(HinhAnhQuangCao anh)
        {
            string uniqueFileName = null;

            if (anh.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "slider");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + anh.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    anh.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }*/
    }
}
