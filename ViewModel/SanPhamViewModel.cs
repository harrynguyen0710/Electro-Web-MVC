using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebDT.ViewModel
{
    public class SanPhamViewModel
    {

        public int MaSanPham { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(100)]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin kích thước sản phẩm")]
        [StringLength(100)]
        public string KichThuoc { get; set; }

        public decimal Gia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? GiaKhuyenMai { get; set; }

        [Required(ErrorMessage = "Vui lòng thêm mô tả")]
        [StringLength(500)]
        public string Mota { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin màn hình")]
        [StringLength(100)]
        public string ManHinh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông số pin")]
        [StringLength(100)]
        public string Pin { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập hệ điều hành")]
        [StringLength(100)]
        public string HeDieuHanh { get; set; }

        [Required(ErrorMessage = "Vui lòng thêm chất liệu sản phẩm")]
        [StringLength(100)]
        public string ChatLieu { get; set; }
        [Required(ErrorMessage = "Vui lòng thêm thông tin camera")]
        [StringLength(100)]
        public string Camera { get; set; }
        [Required(ErrorMessage = "Vui lòng thêm hình ảnh cho sản phẩm")]
        public string HinhAnh { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        [StringLength(100)]
        public string? CPU { get; set; }
        [StringLength(100)]
        public string? Ram { get; set; }
        [StringLength(100)]
        public string? Chip { get; set; }
        [StringLength(100)]
        public string? KhungVien { get; set; }
        public int MaMauSac { get; set; }

        public int MaBoNho { get; set; }
        public List<SanPhamViewModel> SanPhamViewModelList { get; set; }

    }
}

