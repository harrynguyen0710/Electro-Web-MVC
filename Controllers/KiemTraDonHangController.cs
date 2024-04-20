using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DACS.Data;
using DACS.Models;
using DACS.ViewModel;
using DACS.IRepository;

namespace WebDT.Controllers
{
    public class KiemTraDonHangController : Controller
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IDonHang _donHangRepository;
        public KiemTraDonHangController(ApplicationDbContext dataContext, IDonHang donHangRepository)
        {
            _dataContext = dataContext;
            _donHangRepository = donHangRepository; 
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
            var donHang = await _donHangRepository.GetListDonHangByPhoneNum(phoneNumber);
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
            var donHang = await _donHangRepository.GetByIdAsync(maDonHang);
            return View(donHang);
        }

    }
}
