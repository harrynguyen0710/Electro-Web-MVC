using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class ChiTietDonHangSanPham
    {
        [ForeignKey("SanPham")]
        public int MaSanPham { get; set; }
        [ForeignKey("DonHang")]
        public int MaDonHang { get; set; }

        [Required(ErrorMessage = "Vui lòng bổ sung số lượng sản phẩm")]
        public int SoluongMua { get; set; }
        [Required(ErrorMessage = "Vui lòng điền đơn giá bán")]
        public int DonGiaBan { get; set; }

        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
