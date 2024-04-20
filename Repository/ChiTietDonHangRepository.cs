using DACS.Data;
using DACS.IRepository;
using DACS.Models;

namespace DACS.Repository
{
    public class ChiTietDonHangRepository : IGenericRepository<ChiTietDonHangSanPham>
    {
        private readonly ApplicationDbContext _context;
        public ChiTietDonHangRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(ChiTietDonHangSanPham entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddRangeAsync(ChiTietDonHangSanPham[] donHangSanPham)
        {
            await _context.CHITIETDONHANGSANPHAM.AddRangeAsync(donHangSanPham);
            await _context.SaveChangesAsync();
        }

        public Task Delete(ChiTietDonHangSanPham entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChiTietDonHangSanPham>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ChiTietDonHangSanPham> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task Update(ChiTietDonHangSanPham entity)
        {
            throw new NotImplementedException();
        }
    }
}
