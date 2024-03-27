using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DACS.Models
{
    public class HinhAnh
    {
        [Key]
        public int MaHinhAnh { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin loại hình ảnh")]
        public string FileHinhAnh { get; set; }
        //public IFormFile ProfilePhoto { get; set; }
        [ForeignKey("SanPham")]
        public int MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
