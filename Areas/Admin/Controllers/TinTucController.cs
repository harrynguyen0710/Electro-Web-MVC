using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using DACS.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TinTucController : Controller
    {
        private const string FOLDER = "thumbnail";
        private readonly IBlog _blogRepository;
        private readonly IHinhAnh _repository;
        private readonly ApplicationDbContext _context; 
        public TinTucController(IBlog blogRepository, IHinhAnh repository, ApplicationDbContext context)
        {
            _blogRepository = blogRepository;
            _repository = repository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.GetAllAsync();
            return View(blogs);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tinTuc = await _blogRepository.GetByIdAsync(id);

            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }
        public IActionResult Create()
        {
            ViewData["MaChuDe"] = new SelectList(_context.CHUDE, "MaChuDe", "TenChuDe");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TinTuc tinTuc)
        {
            string file = _repository.GetProfilePhotoFileName(tinTuc.ProfilePhoto, FOLDER);
            if (file == null)
            {
                return View(tinTuc);
            }
            tinTuc.HinhAnh = file;

            await _blogRepository.AddAsync(tinTuc);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TINTUC.FindAsync(id);
            if (tinTuc == null)
            {
                return NotFound();
            }
            ViewData["MaChuDe"] = new SelectList(_context.CHUDE, "MaChuDe", "TenChuDe", tinTuc.MaChuDe);
            return View(tinTuc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTinTuc,TieuDe,NoiDung,HinhAnh,MaChuDe")] TinTuc tinTuc)
        {
            if (id != tinTuc.MaTinTuc)
            {
                return NotFound();
            }
            _context.Update(tinTuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TINTUC
                .Include(t => t.ChuDe)
                .FirstOrDefaultAsync(m => m.MaTinTuc == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinTuc = await _context.TINTUC.FindAsync(id);
            if (tinTuc != null)
            {
                _context.TINTUC.Remove(tinTuc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinTucExists(int id)
        {
            return _context.TINTUC.Any(e => e.MaTinTuc == id);
        }

       
    }
}
