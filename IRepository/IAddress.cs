using DACS.Models;

namespace DACS.IRepository
{
    public interface IAddress
    {
        Task<List<Address>> GetAddressesById(string id);
        Task AddAddress(Address address);
        Task RemoveAddress(int id);
    }
}
