using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repository
{
    public class AddressRepository : IAddress
    {
        private ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAddress(Address address)
        {
            await _context.ADDRESS.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task EditAddress(Address address)
        {
            _context.ADDRESS.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _context.ADDRESS.FirstOrDefaultAsync(ad => ad.Id == id);
        }


        public async Task<List<Address>> GetAddressesById(string id)
        {
            return await _context.ADDRESS.Where(u => u.UserId == id).ToListAsync();
        }

        public async Task RemoveAddress(Address address)
        {
            _context.ADDRESS.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}
