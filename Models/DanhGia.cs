using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class DanhGia
    {
        [Key]
        public int MaDanhGia { get; set; }
        [Required(ErrorMessage = "Vui lòng điền mô tả đánh giá")]
        public string MoTaDanhGia { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập điểm đánh giá")]
        public int DiemDanhGia { get; set; }
        public ICollection<BinhLuan> BinhLuan { get; set; }  
    }
}
