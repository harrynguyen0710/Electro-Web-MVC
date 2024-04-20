using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class ChuDe
    {
        [Key]
        public int MaChuDe { get; set; }  
        public string TenChuDe { get; set; }
        public string Mota { get; set; }
        public ICollection<TinTuc> TinTuc { get; set;}

    }
}
