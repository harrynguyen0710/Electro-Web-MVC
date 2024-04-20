using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class TinTuc
    {
        [Key]
        public int MaTinTuc { get; set; }   
        public string TieuDe { get; set; }
        public string NoiDung { get; set; } 
        public string HinhAnh { get; set; }
        [ForeignKey("ChuDe")]
        public int MaChuDe { get; set; }    
        public ChuDe ChuDe { get; set; }
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }
    }
}
