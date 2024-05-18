using DACS.Data;
using DACS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACS.Controllers
{
    public class CompareController : Controller
    {
        private readonly ApplicationDbContext _dataContext;

        public CompareController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index(int? productId1, int? productId2)
        {
            ViewBag.ProductSelectList = new SelectList(_dataContext.SANPHAM, "MaSanPham", "TenSanPham");

            var productsQuery = _dataContext.SANPHAM
                .Include(p => p.HinhAnh)
                .Include(p => p.Ram)
                .Include(p => p.BoNho)
                .Include(p => p.MauSac)
                .AsQueryable();

            var product1 = await productsQuery.FirstOrDefaultAsync(p => p.MaSanPham == productId1);
            var product2 = await productsQuery.FirstOrDefaultAsync(p => p.MaSanPham == productId2);

            List<SanPham> products = new List<SanPham>();
            if (product1 != null && product2 != null)
            {
                products.Add(product1);
                products.Add(product2);
            }

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var product = await _dataContext.SANPHAM
                .Where(p => p.MaSanPham == id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            var hinhAnh = await _dataContext.HINHANH
                .Where(x => x.MaSanPham == product.MaSanPham)
                .Select(x => x.FileHinhAnh)
                .FirstOrDefaultAsync();

            var productDetails = new
            {
                Name = product.TenSanPham,
                Image = hinhAnh,
                Price = product.Gia
            };

            return Json(productDetails);
        }
    }
}
