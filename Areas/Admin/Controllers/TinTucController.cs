using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TinTucController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TinTucController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TinTuc
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TINTUC.Include(t => t.ChuDe);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/TinTuc/Details/5
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

        // GET: Admin/TinTuc/Create
        public IActionResult Create()
        {
            ViewData["MaChuDe"] = new SelectList(_context.CHUDE, "MaChuDe", "MaChuDe");
            return View();
        }

        // POST: Admin/TinTuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTinTuc,TieuDe,NoiDung,HinhAnh,MaChuDe")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tinTuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChuDe"] = new SelectList(_context.CHUDE, "MaChuDe", "MaChuDe", tinTuc.MaChuDe);
            return View(tinTuc);
        }

        // GET: Admin/TinTuc/Edit/5
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
            ViewData["MaChuDe"] = new SelectList(_context.CHUDE, "MaChuDe", "MaChuDe", tinTuc.MaChuDe);
            return View(tinTuc);
        }

        // POST: Admin/TinTuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTinTuc,TieuDe,NoiDung,HinhAnh,MaChuDe")] TinTuc tinTuc)
        {
            if (id != tinTuc.MaTinTuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tinTuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinTucExists(tinTuc.MaTinTuc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChuDe"] = new SelectList(_context.CHUDE, "MaChuDe", "MaChuDe", tinTuc.MaChuDe);
            return View(tinTuc);
        }

        // GET: Admin/TinTuc/Delete/5
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

        // POST: Admin/TinTuc/Delete/5
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
