using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class Wishlist
    {
        [ForeignKey("SanPham")]
        public int ProductId { get; set; }
        public SanPham SanPham { get; set; }    
        [ForeignKey("AppUserModel")]
        public string UserId { get; set; }
        public AppUserModel User { get; set; }
        public int Quantity { get; set; }

    }
}
