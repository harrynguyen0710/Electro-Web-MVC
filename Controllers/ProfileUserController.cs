using DACS.IRepository;
using DACS.Models;
using DACS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Controllers
{
    [Authorize]
    public class ProfileUserController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        public ProfileUserController(UserManager<AppUserModel> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            AccountViewModel account = new AccountViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed
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

    }
}
