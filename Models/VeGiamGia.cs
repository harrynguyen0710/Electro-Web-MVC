using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class VeGiamGia
    {
        [Key]
        public int MaVeGiamGia { get; set; }
        public string Code { get; set; }
        public string NgayThietLap { get; set; }
        public int SoLuongToiDaSuDung { get; set; }

        [ForeignKey("TyLeGiam")]
        public int MaTyLeGiam { get; set; }
        public TyLeGiam TyLeGiam { get; set; }

        public ICollection<DonHang> DonHang { get; set; }
    }
}
