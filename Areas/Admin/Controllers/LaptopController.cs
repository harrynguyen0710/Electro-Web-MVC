using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDT.Data;
using WebDT.Models;
using WebDT.ViewModel;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LaptopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public LaptopController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;

        }


        public async Task<IActionResult> Index()
        {
            var laptop = await _context.LAPTOP.ToListAsync();
            return View(laptop);

        }
        public IActionResult Create()
        {
            ViewBag.BoNho = new SelectList(_context.BONHO, "MaBoNho", "DungLuongBoNho");
            ViewBag.MauSac = new SelectList(_context.MAUSAC, "MaMauSac", "TenMau");
            ViewBag.Ram = new SelectList(_context.RAM, "MaRam", "TenRam");
            ViewBag.LoaiSanPham = new SelectList(_context.LOAISANPHAM, "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.ThuongHieu = new SelectList(_context.THUONGHIEU, "MaThuongHieu", "TenThuongHieu");
            ViewBag.SanPhamDacBiet = new SelectList(_context.SanPhamDacBiet, "MaSanPhamDacBiet", "LoaiSanPhamDacBiet");
            LaptopViewModel lap = new LaptopViewModel();
            return View(lap);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LaptopViewModel lap)
        {
            if (lap == null || lap.Laptop == null)
            {
                return View(lap);
            }

            Laptop mayTinhBang = new Laptop()
            {
                TenSanPham = lap.Laptop.TenSanPham,
                Mota = lap.Laptop.Mota,
                Gia = lap.Laptop.Gia,

                ManHinh = lap.Laptop.ManHinh,
                CPU = lap.Laptop.CPU,
                SoNhanLuong = lap.Laptop.SoNhanLuong,
                VGA = lap.Laptop.VGA,
                TrongLuong = lap.Laptop.TrongLuong,


                MaMauSac = lap.MaMauSac,
                MaBoNho = lap.MaBoNho,
                MaRam = lap.MaRam,
                MaThuongHieu = lap.MaThuongHieu,
                MaLoaiSanPham = lap.MaLoaiSanPham,
                MaSanPhamDacBiet = lap.MaSanPhamDacBiet,


            };
            await _context.SANPHAM.AddAsync(mayTinhBang);
            await _context.SaveChangesAsync();
            foreach (var anh in lap.HinhAnhSanPham)
                {
                    string tenAnh = UploadFile(anh);
                    HinhAnh hinhAnh = new HinhAnh()
                    {
                        FileHinhAnh = tenAnh,
                        MaSanPham = mayTinhBang.MaSanPham
                    };
                    await _context.HINHANH.AddAsync(hinhAnh);
                }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Laptop");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int maSanPham)
        {
     /*       var iphone*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IphoneViewModel iphone)
        {
            return RedirectToAction("Index", "Laptop");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int maSanPham)
        {
            var computer = await _context.LAPTOP.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
            var maRam = await _context.SANPHAM.Where(x => x.MaSanPham == maSanPham)
                                        .Select(m => m.MaRam).FirstOrDefaultAsync();
            var maBoNho = await _context.SANPHAM.Where(x => x.MaSanPham == maSanPham)
                                    .Select(m => m.MaBoNho).FirstOrDefaultAsync();
            var maMauSac = await _context.SANPHAM.Where(x => x.MaSanPham == maSanPham)
                                    .Select(m => m.MaMauSac).FirstOrDefaultAsync();
            var images = await _context.HINHANH.Where(x => x.MaSanPham == maSanPham)
                                .Select(m => m.FileHinhAnh).ToListAsync();
            var boNho = await _context.BONHO.Where(x => x.MaBoNho == maBoNho).Select(t => t.DungLuongBoNho).FirstOrDefaultAsync();
            var mauSac = await _context.MAUSAC.Where(x => x.MaMauSac == maMauSac).Select(t => t.TenMau).FirstOrDefaultAsync();
            var ram = await _context.RAM.Where(x => x.MaRam == maRam).Select(t => t.TenRam).FirstOrDefaultAsync();
            var loaiSanPham = await _context.LOAISANPHAM.Where(x => x.MaLoaiSanPham == computer.MaLoaiSanPham).Select(t => t.TenLoaiSanPham).FirstOrDefaultAsync();
            var thuongHieu = await _context.THUONGHIEU.Where(x => x.MaThuongHieu == computer.MaThuongHieu).Select(t => t.TenThuongHieu).FirstOrDefaultAsync();
            var sanPhamDacBiet = await _context.SanPhamDacBiet.Where(x => x.MaSanPhamDacBiet == computer.MaSanPhamDacBiet).Select(t => t.LoaiSanPhamDacBiet).FirstOrDefaultAsync();
            LaptopViewModel laptop = new LaptopViewModel()
            {
                Laptop = computer,
                DungLuongBoNho = boNho,
                TenMauSac = mauSac,
                TenRam = ram,
                TenThuongHieu = thuongHieu,
                TenLoaiSanPham = loaiSanPham,
                LoaiSanPhamDacBiet = sanPhamDacBiet,
                TenHinhAnh = images
            };
            return View(laptop);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int maSanPham)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(IphoneViewModel iphone)
        {
            return RedirectToAction("Index", "Laptop");
        }


        private string UploadFile(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(_webHost.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
