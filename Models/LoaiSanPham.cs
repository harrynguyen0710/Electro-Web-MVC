using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace DACS.Models
{
    public class LoaiSanPham
    {
        [Key]
        public int MaLoaiSanPham { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin loại sản phẩm")]
        public string TenLoaiSanPham { get; set; }
        public ICollection<SanPham> SanPham { get; set; }
    }
}
