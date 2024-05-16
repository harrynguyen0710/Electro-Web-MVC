using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using DACS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS.Controllers
{
    [Authorize]
    public class ProfileUserController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IAddress _addressRepository;
        private readonly ApplicationDbContext _context;
        public ProfileUserController(UserManager<AppUserModel> userManager, IAddress addressRepository, ApplicationDbContext context)
        {
            _userManager = userManager;
            _addressRepository = addressRepository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var addresses = await _addressRepository.GetAddressesById(user.Id);
            AccountViewModel account = new AccountViewModel()
            {
                UserModel = user,
                Addresses = addresses
            };
            return View(account);
        }
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var addresses = await _addressRepository.GetAddressesById(user.Id);
            AccountViewModel account = new AccountViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                Addresses = addresses
            };
            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AccountViewModel account)
        {
            var user = await _userManager.GetUserAsync(User);

            if (account.NewPassword != null && account.CurrentPassword != null)
            {
                if (account.NewPassword != account.RepeatPassword)
                {
                    ViewBag.ConFirmedPassword = "Change password failed!";
                    return View(account);
                }
                else
                {
                    var result = await _userManager.ChangePasswordAsync(user, account.CurrentPassword, account.NewPassword);
                    if (!result.Succeeded)
                    {
                        ViewBag.ConFirmedPassword = "Change password failed!";
                        return View(account);
                    }
                }
            }

            if (account.Name != null)
            {
                user.Name = account.Name;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", "ProfileUser");
        }
        public ActionResult GetAddress(int id)
        {
            var address =  _context.ADDRESS.Where(ad => ad.Id == id).FirstOrDefault();
            return Json(address);
        }
        public IActionResult InsertAddress()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertAddress(Address address)
        {
            var user = await _userManager.GetUserAsync(User);
            address.UserId = user.Id;
            await _addressRepository.AddAddress(address);
            return RedirectToAction("Index", "ProfileUser");
        }
        [HttpGet]
        public async Task<IActionResult> EditAddress(int id)
        {
            var address = await _addressRepository.GetAddressById(id);
            if (address != null)
            {
                return View(address);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAddress(Address address)
        {
            var user = await _userManager.GetUserAsync(User);
            address.UserId = user.Id;
            await _addressRepository.AddAddress(address);
            return RedirectToAction("Edit", "ProfileUser");
     
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserAddress(int id)
        {
            var address = await _addressRepository.GetAddressById(id);
            await _addressRepository.RemoveAddress(address);
            return RedirectToAction("Edit","ProfileUser");
        }
    }
}
