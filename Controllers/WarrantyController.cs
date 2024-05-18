using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DACS.Controllers
{
    [Authorize]
    public class WarrantyController : Controller
    {
        private readonly IOrderDetails _orderDetails;
        private readonly IToolsRepository<Warranty> _toolsRepository;
        private readonly IWarranty _warrantyRepository;
        public WarrantyController(IOrderDetails orderDetails, IToolsRepository<Warranty> toolsRepository, IWarranty warrantyRepository)
        {
            _orderDetails = orderDetails;
            _toolsRepository = toolsRepository;
            _warrantyRepository = warrantyRepository;
        }

        public async Task<IActionResult> Index()
        {
            var warranty = await _toolsRepository.GetAllAsync();
            return View(warranty);
        }
        public async Task<IActionResult> Create(int productId, int orderId)
        {
            var product = await _orderDetails.GetOrderDetailsById(productId, orderId);
            Warranty warranty = new Warranty
            {
                OrderDetails = product,
            };

            return View(warranty);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Warranty warranty)
        {
            warranty.Date = DateTime.Now;
            await _toolsRepository.AddAsync(warranty);
            return RedirectToAction("WarrantySuccessfully", "Warranty");
        }
        public async Task<IActionResult> Details(int id)
        {
            var warranty = await _warrantyRepository.GetById(id);
            return View(warranty);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var warranty = await _warrantyRepository.GetById(id);
            return View(warranty);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Warranty warranty)
        {

            try
            {
                var existingWarranty = await _warrantyRepository.GetById(warranty.Id);
                if (existingWarranty == null)
                {
                    return NotFound();
                }

                existingWarranty.IsApproved = warranty.IsApproved;
                existingWarranty.Reason = warranty.Reason;

                await _toolsRepository.Update(existingWarranty);
                return RedirectToAction("Index", "Warranty");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            return View(warranty);
        }

        public IActionResult WarrantySuccessfully()
        {
            return View();
        }
    }
}
