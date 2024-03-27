using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class MauSac
    {
        [Key]
        public int MaMauSac { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên màu")]
        public string TenMau { get; set; }
        public IList<SanPham> SanPham { get; set; }
    }
}
