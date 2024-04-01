using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using DACS.Repository;
using DACS.ViewModel;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.Collections.Generic;
using DACS.Service;

namespace WebDT.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IEmailSender _emailSender;

        public CartController(ApplicationDbContext _context, IEmailSender emailSender)
        {
            _dataContext = _context;
            _emailSender = emailSender;

        }

        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>(); // neu co du lieu thi hien thi con khong se tao moi 1 list 
            DonHang donHang = new DonHang();
            decimal tongDonHang = 0;

            foreach (var item in cartItems)
            {
                if (item.GiaKhuyenMai != null)
                {
                    // If GiaKhuyenMai is not null for this item, calculate price using GiaKhuyenMai
                    tongDonHang = (decimal)(tongDonHang + (item.Soluong * item.GiaKhuyenMai));
                }
                else
                {
                    // Otherwise, calculate price using Gia
                    tongDonHang += item.Soluong * item.Gia;
                }
            }
            CartItemViewModel cartVM = new CartItemViewModel
            {
                CartItems = cartItems,
                
/*                GrandTotal = cartItems.Sum(x => x.Soluong * x.Gia),
*/
                GrandTotal = tongDonHang,
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
            TyLeGiam tyLeGiam = new TyLeGiam();

            var maGiamGia = _dataContext.VEGIAMGIA.Where(x => x.Code == cartVM.MaGiamGia).FirstOrDefault();
            if (maGiamGia != null)
            {
                tyLeGiam = _dataContext.TYLEGIAM.Where(x => x.MaTyLeGiam == maGiamGia.MaTyLeGiam).FirstOrDefault();
            }

            var tinhTong = cartItems.Sum(x => x.TongTien);
            if (tyLeGiam == null)
            {

                cartVM.DonHang.TongGiaTriDonHang = tinhTong;
            }
            else
            {
                cartVM.DonHang.TongGiaTriDonHang = tinhTong -   (tinhTong * tyLeGiam.PhanTramGiam) / 100;

            }
            cartVM.DonHang.MaTrangThaiDonHang = 1;
            cartVM.DonHang.MaTrangThaiThanhToan = 2;
            cartVM.DonHang.NgayLapDonHang = DateTime.Now;
            DonHang donHang = cartVM.DonHang;

           
            if (maGiamGia != null)
            {
                donHang.MaVeGiamGia = maGiamGia.MaVeGiamGia;
            }


            
            await _dataContext.DONHANG.AddAsync(donHang);
            await _dataContext.SaveChangesAsync();
            
            foreach(var sanPham in cartItems)
            {
                ChiTietDonHangSanPham ctDonHang = new ChiTietDonHangSanPham()
                {
                    MaDonHang = donHang.MaDonHang,
                    MaSanPham = sanPham.MaSanPham,
                    SoluongMua = sanPham.Soluong,
                };
                if (sanPham.GiaKhuyenMai != null)
                {
                    ctDonHang.DonGiaBan = (decimal)sanPham.GiaKhuyenMai;
                }
                else
                {
                    ctDonHang.DonGiaBan = sanPham.Gia;
                }
                await _dataContext.CHITIETDONHANGSANPHAM.AddAsync(ctDonHang);
                await _dataContext.SaveChangesAsync();
            }
            HttpContext.Session.Remove("Cart");

            MailContent content = new MailContent
            {
                To = "nnhoang0710@gmail.com",
                Subject = "Đơn hàng mới",
                Body = $@"
                        <p><strong>Mã đơn hàng: {donHang.MaDonHang}</strong></p>
                        <p>Khách hàng: {donHang.TenKhachHang}</p>
                        <p>Ngày lập đơn hàng {donHang.NgayLapDonHang}<p/>
                        <p>Số điện thoại: {donHang.SoDienThoai}</p>
                        <p>Địa chỉ: {donHang.DiaChi}</p>
                        <p>Yêu cấu khác: {donHang.YeuCauKhac}</p>
                        <p>Tổng đơn hàng: {cartVM.DonHang.TongGiaTriDonHang}<p>
                    "
            };
            await _emailSender.SendMail(content);


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
