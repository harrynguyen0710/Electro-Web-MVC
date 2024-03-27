using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDT.Data;
using WebDT.Models;
using WebDT.ViewModel;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class iMacController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;


        public iMacController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }


        public async Task<IActionResult> Index(String SearchString)
        {
            var imac = _context.IMAC.AsQueryable();
            if (!string.IsNullOrEmpty(SearchString))
            {
                imac = imac.Where(x => x.TenSanPham.Contains(SearchString));
            }
            return View(imac);

        }
        public IActionResult Create()
        {
            ViewBag.BoNho = new SelectList(_context.BONHO, "MaBoNho", "DungLuongBoNho");
            ViewBag.MauSac = new SelectList(_context.MAUSAC, "MaMauSac", "TenMau");
            ViewBag.Ram = new SelectList(_context.RAM, "MaRam", "TenRam");
            ViewBag.LoaiSanPham = new SelectList(_context.LOAISANPHAM, "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.ThuongHieu = new SelectList(_context.THUONGHIEU, "MaThuongHieu", "TenThuongHieu");
            ViewBag.SanPhamDacBiet = new SelectList(_context.SanPhamDacBiet, "MaSanPhamDacBiet", "LoaiSanPhamDacBiet");
            iMacViewModel ima = new iMacViewModel();
            return View(ima);
        }

        [HttpPost]
        public async Task<IActionResult> Create(iMacViewModel ima)
        {
            if (ima == null || ima.IMac == null)
            {
                return View(ima);
            }

            IMac maytinh = new IMac()
            {
                TenSanPham = ima.IMac.TenSanPham,
                Mota = ima.IMac.Mota,
                Gia = ima.IMac.Gia,
                ManHinh = ima.IMac.ManHinh,



                MaSanPhamDacBiet = ima.MaSanPhamDacBiet,
                MaThuongHieu = ima.MaThuongHieu,
                MaLoaiSanPham = ima.MaLoaiSanPham,
                MaMauSac = ima.MaMauSac,
                MaRam = ima.MaRam,
                MaBoNho = ima.MaBoNho,

                CongNgheCPU = ima.IMac.CongNgheCPU,
                TocDoCPU = ima.IMac.TocDoCPU,
                Turbo = ima.IMac.Turbo,

            };
            await _context.SANPHAM.AddAsync(maytinh);
            await _context.SaveChangesAsync();
            foreach (var anh in ima.HinhAnhSanPham)
                {
                    string tenAnh = UploadFile(anh);
                    HinhAnh hinhAnh = new HinhAnh()
                    {
                        FileHinhAnh = tenAnh,
                        MaSanPham = maytinh.MaSanPham
                    };
                    await _context.HINHANH.AddAsync(hinhAnh);
                }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "iMac");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int maSanPham)
        {

            return RedirectToAction("Index", "iMac");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IphoneViewModel iphone)
        {
            return RedirectToAction("Index", "iMac");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int maSanPham)
        {
            var imac = await _context.IMAC.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
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
            var loaiSanPham = await _context.LOAISANPHAM.Where(x => x.MaLoaiSanPham == imac.MaLoaiSanPham).Select(t => t.TenLoaiSanPham).FirstOrDefaultAsync();
            var thuongHieu = await _context.THUONGHIEU.Where(x => x.MaThuongHieu == imac.MaThuongHieu).Select(t => t.TenThuongHieu).FirstOrDefaultAsync();
            var sanPhamDacBiet = await _context.SanPhamDacBiet.Where(x => x.MaSanPhamDacBiet == imac.MaSanPhamDacBiet).Select(t => t.LoaiSanPhamDacBiet).FirstOrDefaultAsync();

            iMacViewModel iMac = new iMacViewModel()
            {
                IMac = imac,
                DungLuongBoNho = boNho,
                TenMauSac = mauSac,
                TenRam = ram,
                TenThuongHieu = thuongHieu,
                TenLoaiSanPham = loaiSanPham,
                LoaiSanPhamDacBiet = sanPhamDacBiet,
                TenHinhAnh = images
            };
            return View(iMac);
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
