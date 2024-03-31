using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class BinhLuan
    {
        public DateTime ThoiGianBinhLuan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung bình luận")]
        public string NoiDungBinhLuan { get; set; }
        [ForeignKey("DanhGia")]
        public int MaDanhGia { get; set; }
        public DanhGia DanhGia { get; set; }
        [ForeignKey("AppUserModel")]
        public string Id { get; set; }
        public AppUserModel Customer { get; set; }
        [ForeignKey("SanPham")]
        public int MaSanPham { get; set; }
        public SanPham SanPham { get; set; }
    }
}
