using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DACS.Models
{
    public class TrangThaiDonHang
    {
        [Key]
        public int MaTrangThaiDonHang { get; set; }
        public string TenTrangThaiDonHang { get; set; }
        public virtual ICollection<DonHang> DonHang { get; set; }
    }
}
