using DACS.Models;

namespace DACS.IRepository
{
    public interface IProductRepository<T> : IGenericRepository<T> where T : class
    {

        Task<List<T>> GetSanPhamWithImg();
        Task<List<T>> GetSanPhamByBranch(string branch);
        Task<List<T>> GetSanPhamByColor(string color); 
        Task<List<T>> GetSanPhamByType(string type);
    }
}
