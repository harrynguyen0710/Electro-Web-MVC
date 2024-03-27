using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDT.Data;
using WebDT.Models;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ram
        public async Task<IActionResult> Index()
        {
            return View(await _context.RAM.ToListAsync());
        }

        // GET: Ram/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _context.RAM
                .FirstOrDefaultAsync(m => m.MaRam == id);
            if (ram == null)
            {
                return NotFound();
            }

            return View(ram);
        }

        // GET: Ram/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ram/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaRam,TenRam")] Ram ram)
        {
            _context.Add(ram);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ram");

        }

        // GET: Ram/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _context.RAM.FindAsync(id);
            if (ram == null)
            {
                return NotFound();
            }
            return View(ram);
        }

        // POST: Ram/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaRam,TenRam")] Ram ram)
        {
            if (id != ram.MaRam)
            {
                return NotFound();
            }

          
                try

                {
                    _context.Update(ram);
                    await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                    if (!RamExists(ram.MaRam))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            return View(ram);
        }

        // GET: Ram/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ram = await _context.RAM
                .FirstOrDefaultAsync(m => m.MaRam == id);
            if (ram == null)
            {
                return NotFound();
            }

            return View(ram);
        }

        // POST: Ram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ram = await _context.RAM.FindAsync(id);
            if (ram != null)
            {
                _context.RAM.Remove(ram);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RamExists(int id)
        {
            return _context.RAM.Any(e => e.MaRam == id);
        }
    }
}
