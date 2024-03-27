using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebDT.Models;
namespace WebDT.ViewModel
{
    public class IphoneViewModel
    {
        public Iphone Phone { get; set; }
        public int MaLoaiSanPham { get; set; }
        public string TenLoaiSanPham { get; set; }
        public int MaMauSac { get; set;  }
        public string TenMauSac { get; set; }
        public int MaRam { get; set; }
        public string TenRam { get; set; }
        public int MaBoNho { get; set; }
        public string DungLuongBoNho { get; set; }
        public int MaThuongHieu { get; set; }
        public string TenThuongHieu { get; set; }
        public int MaSanPhamDacBiet { get; set; }
        public string LoaiSanPhamDacBiet { get; set; }
        public string PhotoUrl { get; set; }
        public List<IFormFile> HinhAnhSanPham { get; set; }
        public List<string> TenHinhAnh { get;set; }
    }
}
