using DACS.Data;
using DACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACS.MyViewComponent
{
    public class ProductLineViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUserModel> _userManager;
        public ProductLineViewComponent(ApplicationDbContext context, UserManager<AppUserModel> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int maLoaiSanPham, int maMauSac)
        {
            var sanPham = await _context.SANPHAM.Where(m => m.MaLoaiSanPham == maLoaiSanPham)
                .Include(bn => bn.BoNho)
                .Where(c => c.MaMauSac == maMauSac)
                .ToListAsync();
            return View(sanPham);
        }

    }
}
