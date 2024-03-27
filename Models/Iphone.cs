using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DACS.Models
{
    public class Iphone : SanPham
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin Chip")]
        public string Chip { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin ROM")]
        public string Rom { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin Camera trước")]
        public string CameraTruoc { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin Camera sau")]
        public string CameraSau { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thông tin Pin")]
        public string Pin { get; set; }

        public static implicit operator Iphone(List<Iphone> v)
        {
            throw new NotImplementedException();
        }
    }
}
