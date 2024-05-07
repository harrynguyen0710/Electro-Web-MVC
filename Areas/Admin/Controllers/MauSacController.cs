using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.IRepository;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MauSacController : Controller
    {
        private readonly IToolsRepository<MauSac> _toolsRepository;

        public MauSacController(IToolsRepository<MauSac> toolsRepository)
        {
            _toolsRepository = toolsRepository;
        }
        public async Task<IActionResult> Index()
        {
            var mauSac = await _toolsRepository.GetAllAsync();
            return View(mauSac);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauSac = await _toolsRepository.GetByIdAsync(id);
            if (mauSac == null)
            {
                return NotFound();
            }

            return View(mauSac);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMauSac,TenMau")] MauSac mauSac)
        {
            await _toolsRepository.AddAsync(mauSac);  
            return RedirectToAction("Index", "MauSac");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauSac = await _toolsRepository.GetByIdAsync(id);
            if (mauSac == null)
            {
                return NotFound();
            }
            return View(mauSac);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaMauSac,TenMau")] MauSac mauSac)
        {
            if (id != mauSac.MaMauSac)
            {
                return NotFound();
            }
            await _toolsRepository.Update(mauSac);
            return RedirectToAction(nameof(Index));
        }
    }
}
