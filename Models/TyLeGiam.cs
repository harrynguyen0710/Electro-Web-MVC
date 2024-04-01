using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class TyLeGiam
    {
        [Key]
        public int MaTyLeGiam { get; set; }
        public string MoTaTyLeGiam { get; set; }    
        public int PhanTramGiam { get; set; }

        public ICollection<VeGiamGia> VeGiamGia { get; set; }   

    }
}
