using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDT.Data;
using WebDT.Models;
using WebDT.ViewModel;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class iPadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;


        public iPadController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }


        public async Task<IActionResult> Index(String SearchString)
        {
            var ipad = _context.IPAD.AsQueryable();
            if (!string.IsNullOrEmpty(SearchString))
            {
                ipad = ipad.Where(x => x.TenSanPham.Contains(SearchString));
            }
            return View(ipad);

        }
        public IActionResult Create()
        {
            ViewBag.BoNho = new SelectList(_context.BONHO, "MaBoNho", "DungLuongBoNho");
            ViewBag.MauSac = new SelectList(_context.MAUSAC, "MaMauSac", "TenMau");
            ViewBag.Ram = new SelectList(_context.RAM, "MaRam", "TenRam");
            ViewBag.LoaiSanPham = new SelectList(_context.LOAISANPHAM, "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.ThuongHieu = new SelectList(_context.THUONGHIEU, "MaThuongHieu", "TenThuongHieu");
            ViewBag.SanPhamDacBiet = new SelectList(_context.SanPhamDacBiet, "MaSanPhamDacBiet", "LoaiSanPhamDacBiet");
            iPadViewModel ip = new iPadViewModel();
            return View(ip);
        }

        [HttpPost]
        public async Task<IActionResult> Create(iPadViewModel ip)
        {
            if (ip == null || ip.Ipad == null)
            {
                return View();
            }

            Ipad ipa = new Ipad()
            {
                TenSanPham = ip.Ipad.TenSanPham,
                Mota = ip.Ipad.Mota,
                Gia = ip.Ipad.Gia,
                ManHinh = ip.Ipad.ManHinh,


                MaSanPhamDacBiet = ip.MaSanPhamDacBiet,
                MaThuongHieu = ip.MaThuongHieu,
                MaLoaiSanPham = ip.MaLoaiSanPham,
                MaRam = ip.MaRam,
                MaBoNho = ip.MaBoNho,
                MaMauSac = ip.MaMauSac,

                DoPhanGiai = ip.Ipad.DoPhanGiai,
                CongNgheManHinh = ip.Ipad.CongNgheManHinh,
                KichThuocVatLy = ip.Ipad.KichThuocVatLy
            };
            await _context.SANPHAM.AddAsync(ipa);
            await _context.SaveChangesAsync();
            foreach (var anh in ip.HinhAnhSanPham)
                {
                    string tenAnh = UploadFile(anh);
                    HinhAnh hinhAnh = new HinhAnh()
                    {
                        FileHinhAnh = tenAnh,
                        MaSanPham = ipa.MaSanPham
                    };
                    await _context.HINHANH.AddAsync(hinhAnh);
                }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "iPad");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int maSanPham)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IphoneViewModel iphone)
        {
            return RedirectToAction("Index", "Iphone");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int maSanPham)
        {
            var dienThoai = await _context.SANPHAM.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
            var ipad = await _context.IPAD.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
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
            var loaiSanPham = await _context.LOAISANPHAM.Where(x => x.MaLoaiSanPham == ipad.MaLoaiSanPham).Select(t => t.TenLoaiSanPham).FirstOrDefaultAsync();
            var thuongHieu = await _context.THUONGHIEU.Where(x => x.MaThuongHieu == ipad.MaThuongHieu).Select(t => t.TenThuongHieu).FirstOrDefaultAsync();
            var sanPhamDacBiet = await _context.SanPhamDacBiet.Where(x => x.MaSanPhamDacBiet == ipad.MaSanPhamDacBiet).Select(t => t.LoaiSanPhamDacBiet).FirstOrDefaultAsync();

            iPadViewModel ipa = new iPadViewModel()
            {
                Ipad = ipad,
                DungLuongBoNho = boNho,
                TenMauSac = mauSac,
                TenRam = ram,
                TenThuongHieu = thuongHieu,
                TenLoaiSanPham = loaiSanPham,
                LoaiSanPhamDacBiet = sanPhamDacBiet,
                TenHinhAnh = images
            };

            return View(ipa);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int maSanPham)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(IphoneViewModel iphone)
        {
            return RedirectToAction("Index", "Iphone");
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
