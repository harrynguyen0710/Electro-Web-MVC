using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string? NumberAddress { get; set; }
        [ForeignKey("AppUserModel")]
        public string UserId { get; set; }
        public AppUserModel User { get; set; }
    }
}
