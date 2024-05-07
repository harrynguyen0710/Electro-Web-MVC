using System.ComponentModel.DataAnnotations;

namespace DACS.Enum
{
    public enum TrangThaiThanhToan
    {
        [Display(Name = "Unpaid bill")]
        Unpaid,
        [Display(Name = "Paid bill")]
        Paid
    }
}
