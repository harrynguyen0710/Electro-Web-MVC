using DACS.Models;

namespace DACS.IRepository
{
    public interface IWarranty
    {
        Task<Warranty> GetById(int id);
    }
}
