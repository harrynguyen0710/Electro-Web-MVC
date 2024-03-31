/*using DACS.Data;
using DACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Controllers
{
    public class BinhLuanController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly ApplicationDbContext _context;
        public BinhLuanController(UserManager<AppUserModel> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        public IActionResult AddComment()
        {
            return RedirectToAction("Details",);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
*/