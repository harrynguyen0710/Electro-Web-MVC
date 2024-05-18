using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class Warranty
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Date for warranty is required!")]
        public DateTime Date { get; set; }
        [ForeignKey("ChiTietDonHangSanPham")]
        public int ProductId { get; set; }
        [ForeignKey("ChiTietDonHangSanPham")]
        public int OrderId {  get; set; }
        public ChiTietDonHangSanPham OrderDetails { get; set; }
        [Required(ErrorMessage = "Please enter the reason for warranty!")]
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }
}
