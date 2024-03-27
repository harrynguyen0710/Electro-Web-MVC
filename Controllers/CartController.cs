using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDT.Data;
using WebDT.Models;
using WebDT.Repository;
using WebDT.ViewModel;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace WebDT.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dataContext;
        public CartController(ApplicationDbContext _context)
        {
            _dataContext = _context;
        }

        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>(); // neu co du lieu thi hien thi con khong se tao moi 1 list 
            DonHang donHang = new DonHang();

            CartItemViewModel cartVM = new CartItemViewModel
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Soluong * x.Gia),
                TongSoLuongHienThi = cartItems.Sum(x => x.Soluong),
                DonHang = donHang
            };
            return View(cartVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CartItemViewModel cartVM) { 
            cartVM.CartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            if (string.IsNullOrEmpty(cartVM.DonHang.TenKhachHang) || string.IsNullOrEmpty(cartVM.DonHang.SoDienThoai) || string.IsNullOrEmpty(cartVM.DonHang.DiaChi))
        {
                return View(cartVM);
        }
            if (cartVM == null || cartVM.DonHang == null)
            {
                return NotFound();
            }
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>(); // neu co du lieu thi hien thi con khong se tao moi 1 list 
            if (cartItems == null)
            {
                return NotFound();
            }
            cartVM.DonHang.MaTrangThaiDonHang = 5;
            cartVM.DonHang.MaTrangThaiThanhToan = 3;
            cartVM.DonHang.NgayLapDonHang = DateTime.Now;
            DonHang donHang = cartVM.DonHang;
            
            await _dataContext.DonHang.AddAsync(donHang);
            await _dataContext.SaveChangesAsync();
            
            foreach(var sanPham in cartItems)
            {
                ChiTietDonHangSanPham ctDonHang = new ChiTietDonHangSanPham()
                {
                    MaDonHang = donHang.MaDonHang,
                    MaSanPham = sanPham.MaSanPham,
                    SoluongMua = sanPham.Soluong
                };
                await _dataContext.ChiTietDonHangSanPham.AddAsync(ctDonHang);
                await _dataContext.SaveChangesAsync();
            }


            HttpContext.Session.Remove("Cart");
            return RedirectToAction("BuySuccessfully", "Cart");
        }

      
        public async Task<IActionResult> Add(int maSanPham)
        {
            var sanPham = _dataContext.SANPHAM.Where(x => x.MaSanPham == maSanPham).FirstOrDefault();

            if (sanPham != null)
            {
                var hinhAnhList = await _dataContext.HINHANH
                    .Where(x => x.MaSanPham == sanPham.MaSanPham)
                    .ToListAsync();
                var hinhAnh = hinhAnhList[0].FileHinhAnh;
                var viewModel = new CartItemViewModel();
                List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

                CartItemModel cartItem = cart.FirstOrDefault(c => c.MaSanPham == maSanPham);

                if (cartItem == null)
                {
                    cart.Add(new CartItemModel(sanPham, hinhAnh));
                }
                else
                {
                    cartItem.Soluong += 1;
                }

                HttpContext.Session.SetJson("Cart", cart);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }





        public IActionResult Decrease(int maSanPham)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.MaSanPham == maSanPham).FirstOrDefault();
            if(cartItem.Soluong >1){
                --cartItem.Soluong;
            }
            else
            {
                cart.RemoveAll(p => p.MaSanPham == maSanPham);
            }
            if(cart.Count == 0) {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Increase(int maSanPham)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.MaSanPham == maSanPham).FirstOrDefault();
            if (cartItem.Soluong >= 1)
            {
                ++cartItem.Soluong;
            }
            else
            {
                cart.RemoveAll(p => p.MaSanPham == maSanPham);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int maSanPham)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            cart.RemoveAll(x => x.MaSanPham == maSanPham);
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }


        public IActionResult BuySuccessfully()
        {
            return View();
        }
       

    }
}
