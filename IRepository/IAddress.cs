using DACS.Models;

namespace DACS.IRepository
{
    public interface IAddress
    {
        Task<List<Address>> GetAddressesById(string id);
        Task<Address> GetAddressById(int id);
        Task AddAddress(Address address);
        Task RemoveAddress(Address address);
        Task EditAddress(Address address);
    }
}
