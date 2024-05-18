using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.IRepository;
using Microsoft.Extensions.Logging; 


namespace WebDT.Controllers
{
    public class KiemTraDonHangController : Controller
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IDonHang _donHangRepository;
        private readonly ILogger<KiemTraDonHangController> _logger; 


        public KiemTraDonHangController(ApplicationDbContext dataContext, IDonHang donHangRepository, ILogger<KiemTraDonHangController> logger)
        {
            _dataContext = dataContext;
            _donHangRepository = donHangRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ThongTinDonHang(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập số điện thoại.";
                return View("Index");
            }
            var donHang = await _donHangRepository.GetListDonHangByPhoneNum(phoneNumber, true);
            if (donHang.Any())
            {
                return View("ThongTinDonHang", donHang);
            }
            else
            {
                ViewBag.ErrorMessage = "Số điện thoại không khả dụng.";
                return View("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int maDonHang)
        {
            try
            {
                var donHang = await _donHangRepository.GetByIdAsync(maDonHang);
                return View(donHang);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return RedirectToAction("Error", "Home"); 
            }

        }

    }
}
