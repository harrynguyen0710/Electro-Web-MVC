using DACS.Models;

namespace DACS.IRepository
{
    public interface IOrderDetails
    {
        List<ChiTietDonHangSanPham> GetAllOrderDetails(List<CartItemModel> cartItems, DonHang donHang);

        Task AddRangeAsync(ChiTietDonHangSanPham[] items);
        Task<ChiTietDonHangSanPham> GetOrderDetailsById(int productId, int orderId);
    }
}
