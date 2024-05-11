using DACS.Data;
using DACS.IRepository;
using DACS.Models;

namespace DACS.Repository
{
    public class OrderDetailsRepository : IOrderDetails
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task AddRangeAsync(ChiTietDonHangSanPham[] items)
        {
            await _context.AddRangeAsync(items);
            await _context.SaveChangesAsync(); 
        }

        public List<ChiTietDonHangSanPham> GetAllOrderDetails(List<CartItemModel> cartItems, DonHang donHang)
        {
            var orderList = cartItems.Select(sanPham => new ChiTietDonHangSanPham
            {
                MaDonHang = donHang.MaDonHang,
                MaSanPham = sanPham.MaSanPham,
                SoluongMua = sanPham.Soluong,
                DonGiaBan = (sanPham.GiaKhuyenMai != null) ? (decimal)sanPham.GiaKhuyenMai : sanPham.Gia
            }).ToList();

            return orderList;
        }
    }
}
