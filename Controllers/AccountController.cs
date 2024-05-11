using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DACS.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUserModel> _signInManager;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Login(string returnUrl = null)
        {
            LoginViewModel login = new LoginViewModel();
            ViewData["returnUrl"] = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (model == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        var user = await _userManager.FindByNameAsync(model.UserName);
                        var role = await _userManager.GetRolesAsync(user);
                        
                        if (role.Contains("Staff"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            RegisterViewModel register = new RegisterViewModel();
            return View(register);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUserModel { Email = register.Email, UserName = register.UserName, Address = register.DiaChi, PhoneNumber = register.SoDienThoai, Name = register.Name };
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var userRole = await _userManager.FindByIdAsync(user.Id);
                    var customerRole = await _roleManager.FindByNameAsync("User");
                    await _userManager.AddToRoleAsync(userRole,customerRole.Name);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(register);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = "")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "")
        {
            try
            {
                var external = await _signInManager.GetExternalLoginInfoAsync();

                var user = new UserFromExternalDto()
                {
                    Email = external.Principal.FindFirstValue(ClaimTypes.Email),
                    Provider = external.ProviderDisplayName,
                    Name = external.Principal.Identity?.Name ?? ""
                };

                var userExists = await _userManager.FindByEmailAsync(user.Email);

                if (userExists != null)
                {
                    await _signInManager.SignInAsync(userExists, true);
                }
                else
                {
                    var identityUser = new AppUserModel()
                    {
                        Email = user.Email,
                        UserName = user.Email.Split("@")[0],
                        NormalizedEmail = user.Email.ToUpper(),
                        NormalizedUserName = user.Email.Split("@")[0].ToUpper(),

                    };

                    const string defaultPassword = "CodeMega@123";

                    await _userManager.CreateAsync(identityUser, defaultPassword);

                    IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(identityUser, "User");

                    var newUser = await _userManager.FindByEmailAsync(user.Email);

                    await _signInManager.SignInAsync(newUser, true);
                }

                // TO DO HERE
                // Code tiếp phần lưu các thông tin khác người dùng ở đây

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return new RedirectResult($"~/#error-oauth2");
            }
        }



    }
}
