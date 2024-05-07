using System.ComponentModel.DataAnnotations;
namespace DACS.Enum
{
    public enum TrangThaiDonHang
    {
        [Display(Name = "Preparing")]
        Preparing,
        [Display(Name = "Shipping")]
        Shipping,
        [Display(Name = "Received")]
        Received,
        [Display(Name = "Rejected")]
        Rejected
    }
}
