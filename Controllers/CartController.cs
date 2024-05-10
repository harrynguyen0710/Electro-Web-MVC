using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACS.Data;
using DACS.Models;
using DACS.Repository;
using DACS.ViewModel;
using DACS.Service;
using Microsoft.AspNetCore.Identity;
using DACS.IRepository;
using System.Web;

namespace WebDT.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IDonHang _billRepository;
        private readonly IOrderDetails _orderDetailsRepository;
        private readonly IWishListService _wishlistService;


        public CartController(ApplicationDbContext _context, IEmailSender emailSender, 
            UserManager<AppUserModel> userManager, IDonHang billRepository
            , IOrderDetails orderDetailsRepository, IWishListService wishlistService)
        {
            _dataContext = _context;
            _emailSender = emailSender;
            _userManager = userManager;
            _billRepository = billRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            var user = await _userManager.GetUserAsync(User);
            
            DonHang donHang = new DonHang()
            {
                TenKhachHang = user?.Name,
                SoDienThoai = user?.PhoneNumber
            };

            CartItemViewModel cartVM = new CartItemViewModel
            {
                CartItems = cartItems,             
                GrandTotal = _billRepository.GetTotalBill(cartItems),
                TongSoLuongHienThi = cartItems.Sum(x => x.Soluong),
                DonHang = donHang
            };

            return View(cartVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CartItemViewModel cartVM)
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            if (cartVM.DonHang == null || cartItems == null)
            {
                return NotFound();
            }
            var veGiamGia = _dataContext.VEGIAMGIA.Where(x => x.Code == cartVM.Code).FirstOrDefault();
            if(veGiamGia != null)
            {
                cartVM.DonHang.MaVeGiamGia = veGiamGia.MaVeGiamGia;
            }
            else if (veGiamGia == null && cartVM.Code != null)
            {
                ViewBag.InvalidVoucher = "Code voucher is invalid or outdated!";
                cartVM.CartItems = cartItems;
                return View(cartVM);
            }
            cartVM.DonHang.TongGiaTriDonHang = _billRepository.GetTotalBillWithVoucher(cartItems, veGiamGia?.TyleGiam);
            cartVM.DonHang.NgayLapDonHang = DateTime.Now;
            await _billRepository.AddAsync(cartVM.DonHang);
            var orders = _orderDetailsRepository.GetAllOrderDetails(cartItems, cartVM.DonHang);
            await _orderDetailsRepository.AddRangeAsync(orders.ToArray());

            HttpContext.Session.Remove("Cart");

            MailContent content = new MailContent
            {
                To = "nnhoang0710@gmail.com",
                Subject = "Đơn hàng mới",
                Body = $@"
                    <p><strong>Mã đơn hàng: {cartVM.DonHang.MaDonHang}</strong></p>
                    <p>Khách hàng: {cartVM.DonHang.TenKhachHang}</p>
                    <p>Ngày lập đơn hàng: {cartVM.DonHang.NgayLapDonHang}</p>
                    <p>Số điện thoại: {cartVM.DonHang.SoDienThoai}</p>
                    <p>Địa chỉ: {cartVM.DonHang.DiaChi}</p>
                    <p>Yêu cầu khác: {cartVM.DonHang.YeuCauKhac}</p>
                    <p>Tổng đơn hàng: {cartVM.DonHang.TongGiaTriDonHang}</p>"
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

        [HttpGet]
        public async Task<IActionResult> ApplyVoucher(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return Json(new { valid = false, error = "Voucher code is empty." });
                }
                var voucher = await _dataContext.VEGIAMGIA.SingleOrDefaultAsync(v => v.Code == code);

                if (voucher != null)
                {
                    List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                    var totalCost = _billRepository.GetTotalBillWithVoucher(cartItems, voucher?.TyleGiam);
                    return Json(new { valid = true, voucher = new { code = voucher.Code, tyleGiam = voucher.TyleGiam, description = voucher.Mota }, totalCost });
                }
                return Json(new { valid = false, error = "Invalid voucher code." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { valid = false, error = "An error occurred while applying the voucher." });
            }
        }



    }
}
