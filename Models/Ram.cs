using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace DACS.Models
{
    public class Ram
    {
        [Key]
        public int MaRam { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin tên ram")]
        public string TenRam { get; set; }
        public IList<SanPham> SanPham { get; set; }
    }
}
