using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BoNhoController : Controller
    {
        private readonly IToolsRepository<BoNho> _repository;
        public BoNhoController(IToolsRepository<BoNho> repository)
        {
            _repository = repository;
        }


        public async Task<IActionResult> Index()
        {
            var boNho = await _repository.GetAllAsync();
            return View(boNho);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boNho = await _repository.GetByIdAsync(id);
            if (boNho == null)
            {
                return NotFound();
            }

            return View(boNho);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBoNho,DungLuongBoNho")] BoNho boNho)
        {
            await _repository.AddAsync(boNho);
            return RedirectToAction("Index", "BoNho");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boNho = await _repository.GetByIdAsync(id);
            if (boNho == null)
            {
                return NotFound();
            }
            return View(boNho);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBoNho,DungLuongBoNho")] BoNho boNho)
        {
            if (id != boNho.MaBoNho)
            {
                return NotFound();
            }
            await _repository.Update(boNho);
            return RedirectToAction("Index", "BoNho");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boNho = await _repository.GetByIdAsync(id);
            if (boNho == null)
            {
                return NotFound();
            }

            return View(boNho);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var boNho = await _repository.GetByIdAsync(id);
            if (boNho != null)
            {
                await _repository.Delete(boNho);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
