using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DACS.Repository
{
    public class DonHangRepository : IDonHang
    {
        private readonly ApplicationDbContext _context;
        public DonHangRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DonHang>> GetAllAsync()
        {
            return await _context.DONHANG
                .Include(v => v.VeGiamGia)
                .AsSplitQuery()
                .ToListAsync();
           /* return await _context.DONHANG.ToListAsync();*/
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
                .Include(v => v.VeGiamGia)
                .Include(dh => dh.ChiTietDonHangSanPham)
                    .ThenInclude(sp => sp.SanPham)
                        .ThenInclude(anh => anh.HinhAnh)
                .FirstOrDefaultAsync(m => m.MaDonHang == id);

            return donHang;
        }





        public async Task<List<DonHang>> GetListDonHangByPhoneNum(string phoneNum, string? trangThaiDonHang, string? trangThaiThanhToan)
        {
            var query = _context.DONHANG
                .Include(v => v.VeGiamGia)
                .Where(p => p.SoDienThoai == phoneNum);

            if (!string.IsNullOrEmpty(trangThaiDonHang))
            {
                query = query.Where(d => d.TrangThaiDonHang == trangThaiDonHang);
            }

            if (!string.IsNullOrEmpty(trangThaiThanhToan))
            {
                query = query.Where(d => d.TrangThaiThanhToan == trangThaiThanhToan);
            }


            return await query
                .OrderByDescending(d => d.NgayLapDonHang)

                .ToListAsync();
        }


        public decimal GetTotalBill(List<CartItemModel> cartItems)
        {
                decimal tongDonHang = 0;
                foreach (var item in cartItems)
                {
                    if (item.GiaKhuyenMai != null)
                    {
                        tongDonHang = (decimal)(tongDonHang + (item.Soluong * item.GiaKhuyenMai));
                    }
                    else
                    {
                        tongDonHang += item.Soluong * item.Gia;
                    }
                }
                return tongDonHang;
        }
            public decimal GetTotalBillWithVoucher(List<CartItemModel> cartItems, float? tyleGiam)
            {
                var tinhTong = cartItems.Sum(x => x.TongTien);
                if (tyleGiam == null)
                {
                    return tinhTong;
                }

                return tinhTong - (tinhTong * (decimal)tyleGiam) / 100;
            }


        public async Task Update(DonHang donHang)
        {  
                _context.DONHANG.Update(donHang);
                await _context.SaveChangesAsync();
       }

        public Task Delete(DonHang entity)
        {
            throw new NotImplementedException();
        }



/*NOTE HERE*/
        public async Task<PaginatedList<DonHang>> GetPaginatedAsync(int pageIndex, int pageSize)
        {
            var query = _context.DONHANG.AsQueryable();
            return await PaginatedList<DonHang>.CreateAsync(query, pageIndex, pageSize);
        }

        Task<IPagedList<DonHang>> IDonHang.GetPaginatedAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
        public IQueryable<DonHang> GetAll()
        {
            return _context.DONHANG.AsQueryable();
        }


    }
    }

