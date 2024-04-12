using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace DACS.Models
{
    public class SanPham
    {
        [Key]
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Gia { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin màn hình")]
        public string ManHinh { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin mô tả")]
        public string Mota { get; set; }
        
        public int? SoLuongDanhGia { get; set; } 

        public float? DiemDanhGia { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal? GiaKhuyenMai { get; set; }
        public int MaThuongHieu { get; set; }
        [ForeignKey("LoaiSanPham")]
        public int MaLoaiSanPham { get; set; }

        [ForeignKey("MauSac")]
        public int MaMauSac { get; set; }
        public MauSac MauSac { get; set; }

        [ForeignKey("BoNho")]
        public int MaBoNho { get; set; }
        public BoNho BoNho { get; set; }


        [ForeignKey("SanPhamDacBiet")]
        public int MaSanPhamDacBiet { get; set; }
        public SanPhamDacBiet SanPhamDacBiet { get; set; }


        [ForeignKey("Ram")]
        public int MaRam { get; set; }
        public Ram Ram { get; set; }

        
        public ThuongHieu ThuongHieu { get; set; }
        public LoaiSanPham LoaiSanPham { get; set; }
        public ICollection<HinhAnh> HinhAnh { get; set; }
        public ICollection<ChiTietDonHangSanPham> ChiTietDonHangSanPham { get; set; }
        public ICollection<BinhLuan> BinhLuan { get; set; }
    }
}
