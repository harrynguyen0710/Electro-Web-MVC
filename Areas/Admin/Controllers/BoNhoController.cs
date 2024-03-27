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
    public class BoNhoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoNhoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BoNho
        /* public async Task<IActionResult> Index()
         {
             return View(await _context.BONHO.ToListAsync());
         }*/
        public async Task<IActionResult> Index(string searchString)
        {
            var boNho = from b in _context.BONHO
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                boNho = boNho.Where(b => b.DungLuongBoNho.Contains(searchString));
            }

            return View(await boNho.ToListAsync());
        }
        // GET: BoNho/Search
        public async Task<IActionResult> Search(string searchString)
        {
            var boNho = from b in _context.BONHO
                        select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                boNho = boNho.Where(b => b.DungLuongBoNho.Contains(searchString));
            }

            return View("Index", await boNho.ToListAsync());
        }

        // GET: BoNho/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boNho = await _context.BONHO
                .FirstOrDefaultAsync(m => m.MaBoNho == id);
            if (boNho == null)
            {
                return NotFound();
            }

            return View(boNho);
        }

        // GET: BoNho/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BoNho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBoNho,DungLuongBoNho")] BoNho boNho)
        {

            _context.Add(boNho);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "BoNho");

        }

        // GET: BoNho/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boNho = await _context.BONHO.FindAsync(id);
            if (boNho == null)
            {
                return NotFound();
            }
            return View(boNho);
        }

        // POST: BoNho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBoNho,DungLuongBoNho")] BoNho boNho)
        {
            if (id != boNho.MaBoNho)
            {
                return NotFound();
            }
            try
            {
                _context.Update(boNho);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoNhoExists(boNho.MaBoNho))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "BoNho");
        }

        // GET: BoNho/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boNho = await _context.BONHO
                .FirstOrDefaultAsync(m => m.MaBoNho == id);
            if (boNho == null)
            {
                return NotFound();
            }

            return View(boNho);
        }

        // POST: BoNho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boNho = await _context.BONHO.FindAsync(id);
            if (boNho != null)
            {
                _context.BONHO.Remove(boNho);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoNhoExists(int id)
        {
            return _context.BONHO.Any(e => e.MaBoNho == id);
        }
    }
}
