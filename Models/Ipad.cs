using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace DACS.Models
{
    public class Ipad : SanPham
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin Độ phân giải")]
        public string DoPhanGiai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin Công nghệ màn hình")]
        public string CongNgheManHinh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin Kích thước vật lý")]
        public string KichThuocVatLy { get; set; }

    }
}
