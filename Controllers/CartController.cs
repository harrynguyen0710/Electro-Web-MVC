using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using DACS.Repository;
using DACS.ViewModel;
using DACS.Service;
using Microsoft.AspNetCore.Identity;
using DACS.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace WebDT.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AppUserModel> _userManager;

        public CartController(ApplicationDbContext _context, IEmailSender emailSender, UserManager<AppUserModel> userManager)
        {
            _dataContext = _context;
            _emailSender = emailSender;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            var user = await _userManager.GetUserAsync(User);
            DonHang donHang = new DonHang();
            decimal tongDonHang = 0;
            AppUserModel khachHang = user; 
            foreach (var item in cartItems)
            {
                if (item.GiaKhuyenMai != null)
                {
                    tongDonHang = (decimal)(tongDonHang + (item.Soluong * item.GiaKhuyenMai));
                }
                else
                {
                    tongDonHang += item.Soluong * item.Gia;
                }
            }
            CartItemViewModel cartVM = new CartItemViewModel
            {
                CartItems = cartItems,             
/*                GrandTotal = cartItems.Sum(x => x.Soluong * x.Gia), */
                GrandTotal = tongDonHang,
                TongSoLuongHienThi = cartItems.Sum(x => x.Soluong),
                DonHang = donHang,
                KhachHang = khachHang
            };

            return View(cartVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CartItemViewModel cartVM)
        { 
            cartVM.CartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            DonHang donHang = new DonHang();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                cartVM.DonHang.TenKhachHang = user.UserName;
                cartVM.DonHang.SoDienThoai = user.PhoneNumber;
                cartVM.DonHang.DiaChi = user.Address;
            }
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
            donHang = cartVM.DonHang;
            if (maGiamGia != null)
            {
                donHang.MaVeGiamGia = maGiamGia.MaVeGiamGia;
            }
            await _dataContext.DONHANG.AddAsync(donHang);
            await _dataContext.SaveChangesAsync();
            foreach (var sanPham in cartItems)
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
            var sanPham = await _dataContext.SANPHAM.FirstOrDefaultAsync(x => x.MaSanPham == maSanPham);

            if (sanPham != null)
            {
                var hinhAnh = await _dataContext.HINHANH.Where(x => x.MaSanPham == sanPham.MaSanPham)
                                                        .Select(x => x.FileHinhAnh)
                                                        .FirstOrDefaultAsync();

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
            if (cartItem.Soluong > 1)
            {
                --cartItem.Soluong;
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

        [HttpPost("/Cart/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderID);

                // Lưu database đơn hàng của mình

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }

#endregion

        [Authorize]
        public IActionResult PaymentFail()
        {
            return View();
        }

        [Authorize]
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }


            // Lưu đơn hàng vô database

            TempData["Message"] = $"Thanh toán VNPay thành công";
            return RedirectToAction("PaymentSuccess");
        }
    }
}
