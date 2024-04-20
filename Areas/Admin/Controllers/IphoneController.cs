using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DACS.Data;
using DACS.Models;
using DACS.IRepository;


namespace DACS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class IphoneController : Controller
    {
        const string FOLDER = "Images";
        private readonly IToolsRepository<HinhAnh> _genericHinhAnh;
        private readonly IHinhAnh _hinhAnh;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly IProductRepository<Iphone> _iphoneRepository;


        public IphoneController(ApplicationDbContext context, IWebHostEnvironment webHost
            , IHinhAnh hinhAnh, IToolsRepository<HinhAnh> genericHinhAnh
            ,  IProductRepository<Iphone> iphoneRepository)
        {
            _context = context;
            _webHost = webHost;
            _genericHinhAnh = genericHinhAnh;
            _hinhAnh = hinhAnh;
            _iphoneRepository = iphoneRepository;
        }
        public async Task<IActionResult> Index()
        {
            var iphone = await _iphoneRepository.GetAllAsync();
            return View(iphone);

        }
        public IActionResult Create()
        {
            ViewBag.BoNho = new SelectList(_context.BONHO, "MaBoNho", "DungLuongBoNho");
            ViewBag.MauSac = new SelectList(_context.MAUSAC, "MaMauSac", "TenMau");
            ViewBag.Ram = new SelectList(_context.RAM, "MaRam", "TenRam");
            ViewBag.LoaiSanPham = new SelectList(_context.LOAISANPHAM, "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.ThuongHieu = new SelectList(_context.THUONGHIEU, "MaThuongHieu", "TenThuongHieu");
            ViewBag.SanPhamDacBiet = new SelectList(_context.SANPHAMDACBIET, "MaSanPhamDacBiet", "LoaiSanPhamDacBiet");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Iphone iphone)
        {
            if (iphone == null)
            {
                return View(iphone);
            }
            await _iphoneRepository.AddAsync(iphone);
          
            foreach (var anh in iphone.FormFiles)
            {
                string tenAnh = _hinhAnh.GetProfilePhotoFileName(anh, FOLDER);
                HinhAnh hinhAnh = new HinhAnh()
                {
                    FileHinhAnh = tenAnh,
                    MaSanPham = iphone.MaSanPham
                };
                await _genericHinhAnh.AddAsync(hinhAnh);
            }
            return RedirectToAction("Index", "Iphone");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int maSanPham)
        {
            var sanPham = await _iphoneRepository.GetByIdAsync(maSanPham);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }


        [HttpGet]
        public IActionResult Edit(int maSanPham)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Iphone iphone)
        {
            return RedirectToAction("Index", "Iphone");
        }

        [HttpGet]
        public IActionResult Delete(int maSanPham)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete()
        {
            return RedirectToAction("Index", "Iphone");
        }


       
    }
}
