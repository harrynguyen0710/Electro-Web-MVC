using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DACS.Data;
using DACS.Models;
using DACS.IRepository;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LaptopController : Controller
    {
        const string FOLDER = "Images";
        private readonly IToolsRepository<HinhAnh> _genericHinhAnh;
        private readonly IHinhAnh _hinhAnh;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly IProductRepository<Laptop> _laptopRepository;


        public LaptopController(ApplicationDbContext context, IWebHostEnvironment webHost
            , IHinhAnh hinhAnh, IToolsRepository<HinhAnh> genericHinhAnh
            , IProductRepository<Laptop> laptopRepository)
        {
            _context = context;
            _webHost = webHost;
            _genericHinhAnh = genericHinhAnh;
            _hinhAnh = hinhAnh;
            _laptopRepository = laptopRepository;
        }
        public async Task<IActionResult> Index()
        {
            var laptops = await _laptopRepository.GetAllAsync();
            return View(laptops);
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
        public async Task<IActionResult> Create(Laptop laptop)
        {
            if (laptop == null)
            {
                return View(laptop);
            }
            await _laptopRepository.AddAsync(laptop);

            foreach (var anh in laptop.FormFiles)
            {
                string tenAnh = _hinhAnh.GetProfilePhotoFileName(anh, FOLDER);
                HinhAnh hinhAnh = new HinhAnh()
                {
                    FileHinhAnh = tenAnh,
                    MaSanPham = laptop.MaSanPham
                };
                await _genericHinhAnh.AddAsync(hinhAnh);
            }
            return RedirectToAction("Index", "Laptop");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int maSanPham)
        {
            var sanPham = await _laptopRepository.GetByIdAsync(maSanPham);
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
        public IActionResult Edit(Laptop laptop)
        {
            return RedirectToAction("Index", "Laptop");
        }


    }
}
