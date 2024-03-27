using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace DACS.Models
{
    public class IMac : SanPham
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin Công nghệ CPU")]
        public string CongNgheCPU { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin Tốc độ CPU")]
        public string TocDoCPU { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin Tốc độ tối đa")]
        public string Turbo { get; set; }
    }
}
