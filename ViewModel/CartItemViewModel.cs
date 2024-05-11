
using DACS.Models;

namespace DACS.ViewModel
{
    public class CartItemViewModel
    {
        public List<CartItemModel> CartItems { get; set; }
        public DonHang DonHang { get; set; }
        public AppUserModel KhachHang { get; set; }
        public decimal GrandTotal { get; set; }
        public int TongSoLuongHienThi { get; set; } 
        public string Code { get; set; }

    }
}
