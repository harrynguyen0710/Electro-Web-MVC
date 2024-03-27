using WebDT.Data;
using WebDT.Models;
namespace WebDT.ViewModel
{
    public class DonHangViewModel
    {
        public List<DonHang> DonHangList { get; set; }
        public ApplicationDbContext _context { get; set; }
        public List<ChiTietDonHangSanPham> ChiTietDonHangSanPhamList { get; set; }
        public List<TrangThaiDonHang> TrangThaiDonHangList { get; set; }
        public List<TrangThaiThanhToan> TrangThaiThanhToanList { get; set; }
        public List<SanPham> SanPhamList { get; set;}
        public List<HinhAnh> HinhAnhList { get; set; }

        public DonHang DonHang { get; set; }
        public TrangThaiDonHang TrangThaiDonHang { get; set; }
        public TrangThaiThanhToan TrangThaiThanhToan { get; set; }
        public ChiTietDonHangSanPham ChiTietDonHangSanPham { get; set; }
    }
}
