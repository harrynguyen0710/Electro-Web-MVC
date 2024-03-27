using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace DACS.Models
{
    public class BoNho
    {

        [Key]
        public int MaBoNho { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin bộ nhớ")]
        public string DungLuongBoNho { get; set; }
        public IList<SanPham> SanPham { get; set; }

    }
}
