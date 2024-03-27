using System.ComponentModel.DataAnnotations;

namespace  DACS.Models
{
    public class SanPhamDacBiet
    {
        [Key]
        public int MaSanPhamDacBiet { get; set; }   
        public string LoaiSanPhamDacBiet { get; set; }  
        public ICollection<SanPham> SanPham { get; set; }
    }
}
