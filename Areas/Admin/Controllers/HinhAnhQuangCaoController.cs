using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDT.Data;
using WebDT.Models;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HinhAnhQuangCaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        
        public HinhAnhQuangCaoController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var anh = await _context.HinhAnhQuangCao.ToListAsync();
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
            await _context.HinhAnhQuangCao.AddAsync(anh);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","HinhAnhQuangCao");
        }

        public async Task<IActionResult> Delete(int maHinhAnh)
        {
            var anh = await _context.HinhAnhQuangCao.Where(t => t.MaAnhQuangCao == maHinhAnh).FirstOrDefaultAsync();
            return View(anh);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(HinhAnhQuangCao anh)
        {
            _context.HinhAnhQuangCao.Remove(anh);
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
        }
    }
}
