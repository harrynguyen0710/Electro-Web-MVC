using Microsoft.AspNetCore.Identity;

namespace DACS.Models
{
    public class AppUserModel : IdentityUser
    {
        public string Address { get; set; }
        public string? Name { get; set; }    
        
        public ICollection<BinhLuan> BinhLuan { get; set; }
    }
}
