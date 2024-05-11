using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.IRepository
{
    public interface IProductRepository<T> : IGenericRepository<T> where T : class
    {
        Task<List<T>> GetSanPhamWithImg();
        Task<List<T>> GetSanPhamByBranch(string branch);
        Task<List<T>> GetSanPhamByColor(string color);
        Task<List<T>> GetSanPhamByType(string type);
        Task<List<T>> SearchProductsAsync(string keyword); // Thêm phương thức tìm kiếm sản phẩm
    }
}
