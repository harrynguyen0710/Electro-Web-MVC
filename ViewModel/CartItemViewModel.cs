
using DACS.Models;

namespace DACS.ViewModel
{
    public class CartItemViewModel
    {
        public List<CartItemModel> CartItems { get; set; }
        public DonHang DonHang { get; set; }
        public AppUserModel KhachHang { get; set; }
        public decimal GrandTotal { get; set; } //Tinh tong gia
        public int TongSoLuongHienThi { get; set; } 
    }
}
