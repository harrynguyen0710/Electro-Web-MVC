using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repository
{
    public class DonHangRepository :  IDonHang
    {
        private readonly ApplicationDbContext _context;
        public DonHangRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DonHang>> GetAllAsync()
        {
            return await _context.DONHANG
                .Include(tt => tt.TrangThaiDonHang)
                .Include(tt => tt.TrangThaiThanhToan)
                .Include(v => v.VeGiamGia)
                    .ThenInclude(tl => tl.TyLeGiam)
                .ToListAsync();
        }

        public async Task AddAsync(DonHang donHang)
        {
            await _context.DONHANG.AddAsync(donHang);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(DonHang[] entities)
        {
            await _context.DONHANG.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public async Task<DonHang> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var donHang = await _context.DONHANG
                .Include(tt => tt.TrangThaiDonHang)
                .Include(tt => tt.TrangThaiThanhToan)
                .Include(v => v.VeGiamGia)
                    .ThenInclude(tl => tl.TyLeGiam)
                .Include(dh => dh.ChiTietDonHangSanPham)
                    .ThenInclude(sp => sp.SanPham)
                        .ThenInclude(anh => anh.HinhAnh)
                .FirstOrDefaultAsync(m => m.MaDonHang == id);

            return donHang;
        }


        public async Task<List<DonHang>> GetListDonHangByPhoneNum(string phoneNum)
        {
            return await _context.DONHANG
                .Include(tt => tt.TrangThaiDonHang)
                .Include(tt => tt.TrangThaiThanhToan)
                .Include(v => v.VeGiamGia)
                    .ThenInclude(tl => tl.TyLeGiam)
                .Where(p => p.SoDienThoai == phoneNum)
                .ToListAsync();
        }


        public async Task Update(DonHang donHang)
        {
            _context.DONHANG.Update(donHang);
            await _context.SaveChangesAsync();
        }

        Task IGenericRepository<DonHang>.Delete(DonHang entity)
        {
            throw new NotImplementedException();
        }


    }
}
