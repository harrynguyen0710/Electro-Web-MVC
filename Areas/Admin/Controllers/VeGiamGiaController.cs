using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class VeGiamGiaController : Controller
    {
        private readonly IToolsRepository<VeGiamGia> _toolsRepository;
        public VeGiamGiaController(IToolsRepository<VeGiamGia> toolsRepository)
        {
            _toolsRepository = toolsRepository; 
        }
        public async Task<IActionResult> Index()
        {
            var voucher = await _toolsRepository.GetAllAsync();
            return View(voucher);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _toolsRepository.GetByIdAsync(id);  
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaVeGiamGia,Code,NgayThietLap,SoLuongToiDaSuDung,TyLeGiam")] VeGiamGia veGiamGia)
        {
            await _toolsRepository.AddAsync(veGiamGia); 
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _toolsRepository.GetByIdAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaVeGiamGia,Code,NgayThietLap,SoLuongToiDaSuDung,TyLeGiam")] VeGiamGia veGiamGia)
        {
            if (id != veGiamGia.MaVeGiamGia)
            {
                return NotFound();
            }
            await _toolsRepository.Update(veGiamGia);
            return RedirectToAction(nameof(Index));

        }

    }
}
