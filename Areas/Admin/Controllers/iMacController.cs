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
    public class iMacController : Controller
    {
        const string FOLDER = "Images";
        private readonly IToolsRepository<HinhAnh> _genericHinhAnh;
        private readonly IHinhAnh _hinhAnh;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly IProductRepository<IMac> _imacRepository;


        public iMacController(ApplicationDbContext context, IWebHostEnvironment webHost
                   , IHinhAnh hinhAnh, IToolsRepository<HinhAnh> genericHinhAnh
                   , IProductRepository<IMac> imacRepository)
        {
            _context = context;
            _webHost = webHost;
            _genericHinhAnh = genericHinhAnh;
            _hinhAnh = hinhAnh;
            _imacRepository = imacRepository;
        }
        public async Task<IActionResult> Index()
        {
            var imac = await _imacRepository.GetAllAsync();
            return View(imac);
        }
        [HttpGet]
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
        public async Task<IActionResult> Create(IMac imac)
        {
            if (imac == null)
            {
                return NotFound();
            }
            await _imacRepository.AddAsync(imac);

            foreach (var anh in imac.FormFiles)
            {
                string tenAnh = _hinhAnh.GetProfilePhotoFileName(anh, FOLDER);
                HinhAnh hinhAnh = new HinhAnh()
                {
                    FileHinhAnh = tenAnh,
                    MaSanPham = imac.MaSanPham
                };
                await _genericHinhAnh.AddAsync(hinhAnh);
            }
            return RedirectToAction("Index", "iMac");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int maSanPham)
        {
            var sanPham = await _imacRepository.GetByIdAsync(maSanPham);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        [HttpGet]
        public IActionResult Edit(int maSanPham)
        {
            return RedirectToAction("Index", "iMac");
        }

        [HttpPost]
        public IActionResult Edit()
        {
            return RedirectToAction("Index", "iMac");
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
