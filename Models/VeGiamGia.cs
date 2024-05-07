using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class VeGiamGia
    {
        [Key]
        public int MaVeGiamGia { get; set; }
        public string Code { get; set; }
        public DateTime NgayThietLap { get; set; }
        public int SoLuongToiDaSuDung { get; set; }
        public float TyleGiam { get; set; }
        public string Mota { get; set; }
        public ICollection<DonHang> DonHang { get; set; }
    }
}
