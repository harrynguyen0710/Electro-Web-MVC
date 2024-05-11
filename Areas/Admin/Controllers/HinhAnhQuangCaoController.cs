using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.IRepository;

namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HinhAnhQuangCaoController : Controller
    {
        private const string FOLDER = "slider";
        private readonly IToolsRepository<HinhAnhQuangCao> _genericRepository;
        private readonly IHinhAnh _repository;
        public HinhAnhQuangCaoController(IToolsRepository<HinhAnhQuangCao> genericRepository, IHinhAnh repository)
        {
            _genericRepository = genericRepository;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var anh = await _genericRepository.GetAllAsync();
            return View(anh);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HinhAnhQuangCao anh)
        {
            string uniqueFileName = _repository.GetProfilePhotoFileName(anh.ProfilePhoto, FOLDER);
            anh.FileAnh = uniqueFileName;
            await _genericRepository.AddAsync(anh);
            return RedirectToAction("Index", "HinhAnhQuangCao");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int maHinhAnh)
        {
            var anh = await _genericRepository.GetByIdAsync(maHinhAnh);
            if (anh != null)
            {
                await _genericRepository.Delete(anh);
                return RedirectToAction("Index", "HinhAnhQuangCao");
            }
            return View(anh);
        }


      
    }
}
