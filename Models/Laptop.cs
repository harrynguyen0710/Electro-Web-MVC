using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace DACS.Models
{
    public class Laptop : SanPham
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin CPU")]
        public string CPU { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập thông tin số nhân luồng")]
        public string SoNhanLuong { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin VGA")]
        public string VGA { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin trọng lượng")]
        public string TrongLuong { get; set; }
    }
}
