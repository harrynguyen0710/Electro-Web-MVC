using DACS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DACS.Controllers
{
    public class CompareController : Controller
    {
        private readonly ApplicationDbContext _dataContext;
        public CompareController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _dataContext.SANPHAM.ToListAsync();

            ViewBag.Images = new Dictionary<string, object>();
            ViewBag.Price = new Dictionary<string, object>();
            ViewBag.Product = new List<SelectListItem>();

            foreach (var product in products)
            {
                var hinhAnh = await _dataContext.HINHANH
                    .Where(x => x.MaSanPham == product.MaSanPham)
                    .Select(x => x.FileHinhAnh)
                    .FirstOrDefaultAsync();

                ViewBag.Images[product.MaSanPham.ToString()] = hinhAnh;
                ViewBag.Price[product.MaSanPham.ToString()] = product.Gia.ToString();

                ViewBag.Product.Add(new SelectListItem
                {
                    Text = product.TenSanPham,
                    Value = product.MaSanPham.ToString()
                });
            }

            return View(products);
        }


    }
}
