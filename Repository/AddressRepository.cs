using DACS.IRepository;
using DACS.Models;

namespace DACS.Repository
{
    public class AddressRepository : IAddress
    {
        public Task AddAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public Task<List<Address>> GetAddressesById(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}
