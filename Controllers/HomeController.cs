using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using DACS.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DACS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dataContext;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IProductRepository<SanPham> _productRepository;
        private readonly IBlog _blogRepository;
        private readonly IToolsRepository<ChuDe> _topicRepository;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dataContext
            , UserManager<AppUserModel> userManager, IProductRepository<SanPham> productRepository
            , IBlog blogRepository, IToolsRepository<ChuDe> topicRepository)
        {
            _logger = logger;
            _dataContext = dataContext;
            _userManager = userManager;
            _productRepository = productRepository;
            _blogRepository = blogRepository;
            _topicRepository = topicRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sanPham = await _productRepository.GetSanPhamWithImg();
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                // N?u ng??i dùng ?ã ??ng nh?p, g?i thông tin ng??i dùng ??n View
                ViewData["Username"] = currentUser.UserName;
            }
            else
            {
                // N?u không, g?i m?t giá tr? m?c ??nh ho?c null ??n View
                ViewData["Username"] = "Khách";
            }
            return View(sanPham);
        }

        public async Task<IActionResult> Details(int maSanPham)
        {
            var sanPham = await _productRepository.GetByIdAsync(maSanPham);
            SanPhamChiTietViewModel viewModel = new SanPhamChiTietViewModel();
            if (sanPham != null)
            {
                viewModel.SanPham = sanPham;
            }
            ViewBag.DanhSachDanhGia = new SelectList(_dataContext.DANHGIA, "MaDanhGia", "MoTaDanhGia");
            var khachHang = await _userManager.GetUserAsync(User);
            if (khachHang != null)
            {
                viewModel.User = khachHang;
            }
            return View(viewModel);
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
            sanPham.DiemDanhGia = sanPham.DiemDanhGia + diemDanhGia;


            await _dataContext.BINHLUAN.AddAsync(binhLuan);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Details", new { MaSanPham = binhLuan.MaSanPham });

        }

        public async Task<IActionResult> CategoryByBranch(string? branch)
        {
            var sanPham =  await _dataContext.SANPHAM
                                .Include(i => i.HinhAnh)
                                .Where(t => t.ThuongHieu.TenThuongHieu == branch)
                                .ToListAsync();
            return View("Category", sanPham);
        }

        public async Task<IActionResult> CategoryByColor(string? color)
        {
            var sanPham = await _dataContext.SANPHAM
                                .Include(i => i.HinhAnh)
                                .Where(t => t.MauSac.TenMau == color)
                                .ToListAsync();

            return View("Category", sanPham);
        }

        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogRepository.GetAllAsync();
            var topics = await _topicRepository.GetAllAsync();
            ViewData["Topics"] = topics;
            return View(blogs); 
        }
        [HttpGet]
        public async Task<IActionResult> BlogCategory(int maTinTuc)
        {
            var blog = await _blogRepository.GetByIdAsync(maTinTuc);
            if (blog == null) 
            { 
                return NotFound();
            }
            var topics = await _topicRepository.GetAllAsync(); 

            ViewData["Topics"] = topics; 
            return View(blog);

        }

        public async Task<IActionResult> CategoryByProduct(string? category)
        {
            var sanPham = new List<SanPham>();
            if (category == "iphone")
            {
                sanPham = await _dataContext.SANPHAM
                    .Include(i => i.HinhAnh)
                    .Where(s => EF.Property<int>(s, "SanPham") == 1)
                    .ToListAsync();
            }
            else if (category == "ipad")
            {
                sanPham = await _dataContext.SANPHAM.Include(i => i.HinhAnh)
                    .Where(s => EF.Property<int>(s, "SanPham") == 2)
                    .ToListAsync();
            }
            else if (category == "imac")
            {
                sanPham = await _dataContext.SANPHAM.Include(i => i.HinhAnh)
                    .Where(s => EF.Property<int>(s, "SanPham") == 3)
                    .ToListAsync();
            }
            else if (category == "laptop")
            {
                sanPham = await _dataContext.SANPHAM.Include(i => i.HinhAnh)
                    .Where(s => EF.Property<int>(s, "SanPham") == 4)
                    .ToListAsync();
            }
            else
            {
                sanPham = await _dataContext.SANPHAM.Include(i => i.HinhAnh).ToListAsync();
            }


            return View("Category", sanPham);
        }
        public async Task<IActionResult> SearchProducts(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    // N?u t? khóa tìm ki?m tr?ng, tr? v? t?t c? s?n ph?m
                    var allProducts = await _productRepository.GetAllAsync();
                    return Json(allProducts); // Tr? v? danh sách s?n ph?m d??i d?ng JSON
                }
                else
                {
                    // Tìm ki?m s?n ph?m theo t? khóa
                    var searchResults = await _productRepository.SearchProductsAsync(keyword);
                    return Json(searchResults); // Tr? v? k?t qu? tìm ki?m d??i d?ng JSON
                }
            }
            catch (Exception ex)
            {
                // X? lý các l?i x?y ra trong quá trình tìm ki?m và tr? v? mã l?i 500 (Internal Server Error)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        public async Task<IActionResult> TopicsPartial()
        {
            var topics = await _topicRepository.GetAllAsync();
            return PartialView("_TopicsPartial", topics);
        }

        public IActionResult CSKH()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
