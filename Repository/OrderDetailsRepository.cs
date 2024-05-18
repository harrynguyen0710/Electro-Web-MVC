using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ChiTietDonHangSanPham> GetOrderDetailsById(int productId, int orderId)
        {
            return await _context.CHITIETDONHANGSANPHAM
                          .Include(s => s.SanPham)
                            .ThenInclude(i => i.HinhAnh)
                          .Include(d => d.DonHang)
                          .FirstOrDefaultAsync(p => p.MaSanPham == productId && p.MaDonHang == orderId);
        }
    }
}
