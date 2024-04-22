using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        const string FOLDER = "thumbnail";
        private readonly ApplicationDbContext _context;
        private readonly IHinhAnh _repository;

        public TinTucController(ApplicationDbContext context, IHinhAnh repository)
        {
            _context = context;
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TINTUC.Include(t => t.ChuDe);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
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
        public IActionResult Create()
        {
            ViewData["MaChuDe"] = new SelectList(_context.CHUDE, "MaChuDe", "TenChuDe");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTinTuc,TieuDe,NoiDung,HinhAnh,MaChuDe")] TinTuc tinTuc)
        {
            string file = _repository.GetProfilePhotoFileName(tinTuc.ProfilePhoto, FOLDER);
            tinTuc.HinhAnh = file ?? "";

            await _context.TINTUC.AddAsync(tinTuc);
            await _context.SaveChangesAsync();
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
