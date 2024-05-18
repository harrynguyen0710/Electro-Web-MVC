using DACS.Models;

namespace DACS.IRepository
{
    public interface IWarranty
    {
        Task<List<Warranty>> GetAllByPhoneNumber(string phoneNumber, bool status);
        Task<Warranty> GetById(int id);
    }
}
