using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using DACS.Data;
using DACS.Models;
using DACS.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DACS.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dataContext;
        private readonly UserManager<AppUserModel> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dataContext, UserManager<AppUserModel> userManager)
        {
            _logger = logger;
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? searchString)
        {
            var sanPhamList = await _dataContext.SANPHAM.ToListAsync();

            // Filter based on searchString
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPhamList = sanPhamList.Where(x => x.TenSanPham.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var hinhAnhList = await _dataContext.HINHANH.ToListAsync();
            var hinhAnhQuangCao = await _dataContext.HINHANHQUANGCAO.ToListAsync();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList,
                HinhAnhQuangCao = hinhAnhQuangCao
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int maSanPham)
        {
            var sanPham = await _dataContext.SANPHAM.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
            var danhSachLoaiSanPham = await _dataContext.SANPHAM.Where(x => x.MaLoaiSanPham == sanPham.MaLoaiSanPham).ToListAsync();
            var hinhAnhList = await _dataContext.HINHANH.Where(x => x.MaSanPham == sanPham.MaSanPham).ToListAsync();

            // key
            var maThuongHieu = await _dataContext.SANPHAM.Where(x => x.MaSanPham == maSanPham).Select(m => m.MaThuongHieu).FirstOrDefaultAsync();

            var maBoNho = await _dataContext.SANPHAM.Where(x => x.MaSanPham == maSanPham).Select(m => m.MaBoNho).FirstOrDefaultAsync();

            var maMauSac = await _dataContext.SANPHAM.Where(x => x.MaSanPham == maSanPham).Select(m => m.MaMauSac).FirstOrDefaultAsync();

            var maRam = await _dataContext.SANPHAM.Where(x => x.MaSanPham == maSanPham).Select(m => m.MaRam).FirstOrDefaultAsync();
            // object
            var thuongHieu = await _dataContext.THUONGHIEU.Where(x => x.MaThuongHieu == maThuongHieu).FirstOrDefaultAsync();

            var boNho = await _dataContext.BONHO.Where(x => x.MaBoNho == maBoNho).FirstOrDefaultAsync();

            var mauSac = await _dataContext.MAUSAC.Where(x => x.MaMauSac == maMauSac).FirstOrDefaultAsync();

            var ram = await _dataContext.RAM.Where(x => x.MaRam == maRam).FirstOrDefaultAsync();

            // BinhLuan
            var binhLuan = await _dataContext.BINHLUAN.Where(x => x.MaSanPham == maSanPham).ToListAsync();


            var viewModel = new SanPhamChiTietViewModel();
            if (sanPham is Iphone)
            {
                Iphone product = await _dataContext.IPHONE.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
                viewModel = new SanPhamChiTietViewModel
                {
                    Iphone = product,
                };
            }
            else if (sanPham is Laptop)
            {
                Laptop lap = await _dataContext.LAPTOP.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
                viewModel = new SanPhamChiTietViewModel
                {
                    Laptop = lap,
                };
            }
            else if (sanPham is IMac)
            {
                IMac imac = await _dataContext.IMAC.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
                viewModel = new SanPhamChiTietViewModel
                {
                    Imac = imac,
                };
            }
            else if (sanPham is Ipad)
            {
                Ipad ipadPr = await _dataContext.IPAD.Where(x => x.MaSanPham == maSanPham).FirstOrDefaultAsync();
                viewModel = new SanPhamChiTietViewModel
                {
                    Ipad = ipadPr,
                };
            }

            viewModel.SanPhamList = danhSachLoaiSanPham;
            viewModel.HinhAnhList = hinhAnhList;
            viewModel.SanPham = sanPham;

            viewModel.MauSac = mauSac;

            viewModel.Ram = ram;
            viewModel.ThuongHieu = thuongHieu;
            viewModel.BoNho = boNho;
            viewModel.context = _dataContext;

            viewModel.BinhLuanList = binhLuan;
            if (binhLuan == null)
            {
                return NotFound();
            }

            ViewBag.DanhSachDanhGia = new SelectList(_dataContext.DANHGIA, "MaDanhGia", "MoTaDanhGia");

            /*RANG BUOC KHACH HANG CHI DANH GIA DC 1 LAN*/
            var khachHang = await _userManager.GetUserAsync(User);
            viewModel.User = khachHang;

            return View(viewModel);
        }
        public IActionResult XemThemSanPhamMoi(int maSanPhamDacBiet)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaSanPhamDacBiet == maSanPhamDacBiet).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemSanPhamMoi", viewModel);
        }
        public IActionResult XemThemSanPhamYeuThich(int maSanPhamDacBiet)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaSanPhamDacBiet == maSanPhamDacBiet).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemSanPhamYeuThich", viewModel);
        }
        public IActionResult XemThemSanPhamBanChay(int maSanPhamDacBiet)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaSanPhamDacBiet == maSanPhamDacBiet).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemSanPhamBanChay", viewModel);
        }
        public async Task<IActionResult> XemThemSanPhamIphone()
        {

            var iphone = await _dataContext.IPHONE.ToListAsync();
            var hinhAnhList = await _dataContext.HINHANH.ToListAsync();

            var viewModel = new SanPhamChiTietViewModel
            {
                IphoneList = iphone,
                HinhAnhList = hinhAnhList
            };

            return View(viewModel);
        }
        public IActionResult XemThemAppleProduct(int maThuongHieu)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaThuongHieu == maThuongHieu).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemAppleProduct", viewModel);
        }
        public IActionResult XemThemAsusProduct(int maThuongHieu)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaThuongHieu == maThuongHieu).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemAsusProduct", viewModel);
        }
        public IActionResult XemThemLenovoProduct(int maThuongHieu)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaThuongHieu == maThuongHieu).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemLenovoProduct", viewModel);
        }
        public IActionResult XemThemMSIProduct(int maThuongHieu)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaThuongHieu == maThuongHieu).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemMSIProduct", viewModel);
        }
        public IActionResult XemThemBlackProduct(int maMauSac)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaMauSac == maMauSac).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemBlackProduct", viewModel);
        }
        public IActionResult XemThemWhiteProduct(int maMauSac)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaMauSac == maMauSac).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };


            // Truy?n ViewModel vào View
            return View("XemThemWhiteProduct", viewModel);
        }
        public IActionResult XemThemGreyProduct(int maMauSac)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaMauSac == maMauSac).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };
            // Truy?n ViewModel vào View
            return View("XemThemGreyProduct", viewModel);
        }
        public IActionResult XemThemBlueProduct(int maMauSac)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaMauSac == maMauSac).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemBlueProduct", viewModel);
        }
        public IActionResult XemThemPinkProduct(int maMauSac)
        {
            var sanPhamList = _dataContext.SANPHAM.Where(x => x.MaMauSac == maMauSac).ToList();
            var hinhAnhList = _dataContext.HINHANH.ToList();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View("XemThemPinkProduct", viewModel);
        }
        public async Task<IActionResult> XemThemSanPhamIpad()
        {

            var ipad = await _dataContext.IPAD.ToListAsync();
            var hinhAnhList = await _dataContext.HINHANH.ToListAsync();

            var viewModel = new SanPhamChiTietViewModel
            {
                IpadList = ipad,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View(viewModel);
        }
        public async Task<IActionResult> XemThemSanPhamIMac()
        {

            var imac = await _dataContext.IMAC.ToListAsync();
            var hinhAnhList = await _dataContext.HINHANH.ToListAsync();

            var viewModel = new SanPhamChiTietViewModel
            {
                IMacList = imac,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View(viewModel);
        }
        public async Task<IActionResult> XemThemSanPhamLaptop()
        {

            var laptop = await _dataContext.LAPTOP.ToListAsync();
            var hinhAnhList = await _dataContext.HINHANH.ToListAsync();

            var viewModel = new SanPhamChiTietViewModel
            {
                LaptopList = laptop,
                HinhAnhList = hinhAnhList
            };

            // Truy?n ViewModel vào View
            return View(viewModel);
        }

        public IActionResult CSKH()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(BinhLuan binhLuan)
        {
            if (binhLuan == null)
            {
                return NotFound();
            }
            var khachHangDanhGia = await _userManager.GetUserAsync(User);
            binhLuan.ThoiGianBinhLuan = DateTime.Now;
            binhLuan.Id = khachHangDanhGia.Id;

            if (khachHangDanhGia == null)
            {
                return NotFound();
            }
            var diemDanhGia = _dataContext.DANHGIA.Where(m => m.MaDanhGia == binhLuan.MaDanhGia).Select(d => d.DiemDanhGia).FirstOrDefault();

            var sanPham = _dataContext.SANPHAM.Where(m => m.MaSanPham == binhLuan.MaSanPham).FirstOrDefault();
            if (sanPham.DiemDanhGia == null)
            {
                sanPham.DiemDanhGia = 0;
            }
            if (sanPham.SoLuongDanhGia == null)
            {
                sanPham.SoLuongDanhGia = 0;

            }

            sanPham.SoLuongDanhGia = sanPham.SoLuongDanhGia + 1;
            sanPham.DiemDanhGia = sanPham.DiemDanhGia +  diemDanhGia;


            await _dataContext.BINHLUAN.AddAsync(binhLuan);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", new { MaSanPham = binhLuan.MaSanPham });

        }

        public async Task<IActionResult> ShopCategory(int id, string? category, float? minPrice, float? maxPrice)
        {
            var sanPhamQuery = _dataContext.SANPHAM.AsQueryable();
            var sanPhamList = await sanPhamQuery.ToListAsync();
            var hinhAnhList = await _dataContext.HINHANH.ToListAsync();

            var viewModel = new SanPhamChiTietViewModel
            {
                SanPhamList = sanPhamList,
                HinhAnhList = hinhAnhList
            };

            return View("ShopCategory", viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
