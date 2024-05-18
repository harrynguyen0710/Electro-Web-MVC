using DACS.Data;
using DACS.IRepository;
using DACS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDT.Controllers;

namespace DACS.Controllers
{
    public class WarrantyCheckController : Controller
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IWarranty _warrantyRepository;
        public WarrantyCheckController(ApplicationDbContext dataContext, IWarranty warranty) 
        {
            _dataContext = dataContext; 
            _warrantyRepository = warranty;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> WarrantyInformation(string phoneNumber, bool status)
        {
            ViewBag.phoneNumber = phoneNumber;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập số điện thoại.";
                return View("Index");
            }
/*            var warranty = await _warrantyRepository.GetAllByPhoneNumber(phoneNumber, status);
*/
            var warranty = await _dataContext.WARRANTY
                         .Include(p => p.OrderDetails)
                         .ThenInclude(od => od.DonHang)
                         .Where(p => p.OrderDetails.DonHang.SoDienThoai == phoneNumber)
                         .ToListAsync();

            if (warranty != null)
            {
                return View("WarrantyInformation",warranty);
            }
            else
            {
                ViewBag.ErrorMessage = "Số điện thoại không khả dụng.";
                return View("Index");
            }
        }

    }
}
