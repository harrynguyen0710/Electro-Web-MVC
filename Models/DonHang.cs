using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DACS.Models
{
    public class DonHang
    {
        [Key]
        public int MaDonHang { get; set; }
        public DateTime NgayLapDonHang { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TongGiaTriDonHang { get; set; }

        [ForeignKey("TrangThaiThanhToan")]
        public int MaTrangThaiThanhToan { get; set; }
        [ForeignKey("TrangThaiDonHang")]
        public int MaTrangThaiDonHang { get; set; }
        [ForeignKey("VeGiamGia")]
        public int MaVeGiamGia { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SoDienThoai { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string DiaChi { get; set; }
        public string? YeuCauKhac { get; set; }


        public VeGiamGia VeGiamGia { get; set; }

        public TrangThaiDonHang TrangThaiDonHang { get; set; }
        public TrangThaiThanhToan TrangThaiThanhToan { get; set; }

        public ICollection<ChiTietDonHangSanPham> ChiTietDonHangSanPham { get; set; }
    }
}
