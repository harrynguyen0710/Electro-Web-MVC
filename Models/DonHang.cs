using DACS.Enum;
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

        [ForeignKey("VeGiamGia")]
        public int? MaVeGiamGia { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string SoDienThoai { get; set; }


        [Required(ErrorMessage = "Please enter your address")]
        public string DiaChi { get; set; }
        public string? YeuCauKhac { get; set; }


        public VeGiamGia VeGiamGia { get; set; }

        public string TrangThaiDonHang { get; set; }
        public string TrangThaiThanhToan { get; set; }

        public ICollection<ChiTietDonHangSanPham> ChiTietDonHangSanPham { get; set; }
    }
}
