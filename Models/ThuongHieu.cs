using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace DACS.Models
{
    public class ThuongHieu
    {
        [Key]
        public int MaThuongHieu { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin tên thương hiệu")]
        public string TenThuongHieu { get; set; }
        public ICollection<SanPham> SanPham { get; set; }
    }
}
