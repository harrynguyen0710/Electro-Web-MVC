using Microsoft.AspNetCore.Identity;

namespace DACS.Models
{
    public class AppUserModel : IdentityUser
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<BinhLuan> BinhLuan { get; set; }
        public ICollection<Wishlist> WishList { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
