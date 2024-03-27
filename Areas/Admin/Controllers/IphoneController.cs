using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDT.Data;
using WebDT.Models;
using WebDT.ViewModel;
using static WebDT.Models.SanPham;

namespace WebDT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class IphoneController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public IphoneController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;

        }
        public async Task<IActionResult> Index(String SearchString)
        {
            var iphone = _context.IPHONE.AsQueryable();
            if (!string.IsNullOrEmpty(SearchString))
            {
                iphone = iphone.Where(x => x.TenSanPham.Contains(SearchString));
            }
            return View(iphone);

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.BoNho = new SelectList(_context.BONHO, "MaBoNho", "DungLuongBoNho");
            ViewBag.MauSac = new SelectList(_context.MAUSAC, "MaMauSac", "TenMau");
            ViewBag.Ram = new SelectList(_context.RAM, "MaRam", "TenRam");
            ViewBag.LoaiSanPham = new SelectList(_context.LOAISANPHAM, "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.ThuongHieu = new SelectList(_context.THUONGHIEU, "MaThuongHieu", "TenThuongHieu");
            ViewBag.SanPhamDacBiet = new SelectList(_context.SanPhamDacBiet, "MaSanPhamDacBiet", "LoaiSanPhamDacBiet");
            IphoneViewModel iphone = new IphoneViewModel();
            return View(iphone);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IphoneViewModel iphone)
        {
            if (iphone == null || iphone.Phone == null)
            {
                return View(iphone);
            }

            Iphone dienThoai = new Iphone()
            {
                TenSanPham = iphone.Phone.TenSanPham,
                Mota = iphone.Phone.Mota,
                Gia = iphone.Phone.Gia,
                ManHinh = iphone.Phone.ManHinh,


                MaSanPhamDacBiet = iphone.MaSanPhamDacBiet,
                MaThuongHieu = iphone.MaThuongHieu,
                MaLoaiSanPham = iphone.MaLoaiSanPham,
                MaRam = iphone.MaRam,
                MaBoNho = iphone.MaBoNho,
                MaMauSac = iphone.MaMauSac,

                Chip = iphone.Phone.Chip,
                Rom = iphone.Phone.Rom,
                CameraTruoc = iphone.Phone.CameraTruoc,
                CameraSau = iphone.Phone.CameraSau,
                Pin = iphone.Phone.Pin
            };

            await _context.SANPHAM.AddAsync(dienThoai);
            await _context.SaveChangesAsync();
            foreach (var anh in iphone.HinhAnhSanPham)
                {
                    string tenAnh = UploadFile(anh);
                    HinhAnh hinhAnh = new HinhAnh()
                    {
                        FileHinhAnh = tenAnh,
                        MaSanPham = dienThoai.MaSanPham
                    };
                    await _context.HINHANH.AddAsync(hinhAnh);
                }
            int thoiGianBaoHanh = int.Parse(Request.Form["ThoiGianBaoHanh"]);
            ViewBag.ThoiGianBaoHanh = Enum.GetValues(typeof(ThoiGianBaoHanhEnum))
                                    .Cast<ThoiGianBaoHanhEnum>()
                                    .Select(v => new SelectListItem
                                    {
                                        Text = v.ToString().Substring(1) + " tháng",
                                        Value = ((int)v).ToString()
                                    });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Iphone");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int maSanPham)
        {
            ViewBag.BoNho = new SelectList(_context.BONHO, "MaBoNho", "DungLuongBoNho");
            ViewBag.MauSac = new SelectList(_context.MAUSAC, "MaMauSac", "TenMau");
            ViewBag.Ram = new SelectList(_context.RAM, "MaRam", "TenRam");
            ViewBag.LoaiSanPham = new SelectList(_context.LOAISANPHAM, "MaLoaiSanPham", "TenLoaiSanPham");
            ViewBag.ThuongHieu = new SelectList(_context.THUONGHIEU, "MaThuongHieu", "TenThuongHieu");

            var dienThoai = await _context.SANPHAM.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
            var phone = await _context.IPHONE.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
            var maRam = await _context.SANPHAM.Where(x => x.MaSanPham == maSanPham)
                                        .Select(m => m.MaRam).FirstOrDefaultAsync();
            var maBoNho = await _context.SANPHAM.Where(x => x.MaSanPham == maSanPham)
                                    .Select(m => m.MaBoNho).FirstOrDefaultAsync();
            var maMauSac = await _context.SANPHAM.Where(x => x.MaSanPham == maSanPham)
                                    .Select(m => m.MaMauSac).FirstOrDefaultAsync();
            var images = await _context.HINHANH.Where(x => x.MaSanPham == maSanPham)
                                .Select(m => m.FileHinhAnh).ToListAsync();

            IphoneViewModel iphone = new IphoneViewModel()
            {
                Phone = phone,
                MaBoNho = maBoNho,
                MaMauSac = maMauSac,
                MaRam = maRam,
                MaThuongHieu = phone.MaThuongHieu,
                MaLoaiSanPham = phone.MaLoaiSanPham,
                TenHinhAnh = images
            };
            return View(iphone);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IphoneViewModel iphone)
        {
            Iphone dienThoai = new Iphone()
            {
                MaSanPham = iphone.Phone.MaSanPham,
                TenSanPham = iphone.Phone.TenSanPham,
                Mota = iphone.Phone.Mota,
                Gia = iphone.Phone.Gia,
                ManHinh = iphone.Phone.ManHinh,

                MaLoaiSanPham = iphone.MaLoaiSanPham,
                MaThuongHieu = iphone.MaThuongHieu,
                MaRam = iphone.MaRam,
                MaBoNho = iphone.MaBoNho,
                MaMauSac = iphone.MaMauSac,


                Chip = iphone.Phone.Chip,
                Rom = iphone.Phone.Rom,
                CameraTruoc = iphone.Phone.CameraTruoc,
                CameraSau = iphone.Phone.CameraSau,
                Pin = iphone.Phone.Pin
            };
            _context.Attach(dienThoai);
            _context.Entry(dienThoai).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Iphone");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int maSanPham)
        {
            var phone = await _context.IPHONE.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
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
            var loaiSanPham = await _context.LOAISANPHAM.Where(x => x.MaLoaiSanPham == phone.MaLoaiSanPham).Select(t => t.TenLoaiSanPham).FirstOrDefaultAsync();
            var thuongHieu = await _context.THUONGHIEU.Where(x => x.MaThuongHieu == phone.MaThuongHieu).Select(t => t.TenThuongHieu).FirstOrDefaultAsync();
            var sanPhamDacBiet = await _context.SanPhamDacBiet.Where(x => x.MaSanPhamDacBiet == phone.MaSanPhamDacBiet).Select(t => t.LoaiSanPhamDacBiet).FirstOrDefaultAsync();
            IphoneViewModel iphone = new IphoneViewModel()
            {
                Phone = phone,
                DungLuongBoNho = boNho,
                TenMauSac = mauSac,
                TenRam = ram,
                TenThuongHieu = thuongHieu,
                TenLoaiSanPham = loaiSanPham,
                LoaiSanPhamDacBiet = sanPhamDacBiet,
                TenHinhAnh = images
            };
            return View(iphone);
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
