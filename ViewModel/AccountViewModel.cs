using DACS.Models;

namespace DACS.ViewModel
{
    public class AccountViewModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }    
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public AppUserModel UserModel { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
