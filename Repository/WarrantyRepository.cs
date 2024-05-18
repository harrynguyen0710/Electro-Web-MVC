using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repository
{
    public class WarrantyRepository : IWarranty
    {
        private readonly ApplicationDbContext _context;
        public WarrantyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Warranty> GetById(int id)
        {
            return await _context.WARRANTY
                .Include(w => w.OrderDetails)
                    .ThenInclude(od => od.SanPham)
                .Include(w => w.OrderDetails)
                    .ThenInclude(od => od.DonHang)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Warranty>> GetAllByPhoneNumber(string phoneNumber, bool status)
        {
            if(status == null)
            {
                return await _context.WARRANTY
                         .Include(p => p.OrderDetails)
                         .ThenInclude(od => od.DonHang)
                         .Where(p => p.OrderDetails.DonHang.SoDienThoai == phoneNumber)
                         .ToListAsync();
            }
            var warranty = await _context.WARRANTY
                         .Include(p => p.OrderDetails)
                         .ThenInclude(od => od.DonHang)
                         .Where(p => p.OrderDetails.DonHang.SoDienThoai == phoneNumber
                                     && p.IsApproved == status)
                         .ToListAsync();

            return warranty;
        }


    }
}
