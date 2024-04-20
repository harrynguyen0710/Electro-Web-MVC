using DACS.Data;
using DACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS.MyViewComponent
{
    public class UserReviewViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUserModel> _userManager;

        public UserReviewViewComponent(ApplicationDbContext context, UserManager<AppUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maSanPham)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var binhLuanNguoidung = _context.BINHLUAN.Where(x => x.MaSanPham == maSanPham)
                .Include(r => r.DanhGia)
                .FirstOrDefault(m => m.Id == currentUser.Id);

            return View(binhLuanNguoidung);
        }
    }

}
