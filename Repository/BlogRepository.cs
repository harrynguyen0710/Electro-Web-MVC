using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repository
{
    public class BlogRepository : IBlog
    {
        private readonly ApplicationDbContext _context;
        public BlogRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task AddAsync(TinTuc entity)
        {
            await _context.TINTUC.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(TinTuc entity)
        {
            _context.TINTUC.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TinTuc>> GetAllAsync()
        {
            return await _context.TINTUC
                .Include(t => t.ChuDe)
                .Select(t => new TinTuc
                {
                    MaTinTuc = t.MaTinTuc,
                    TieuDe = t.TieuDe,
                    TomTat = t.TomTat,
                    HinhAnh = t.HinhAnh,
                })
                .ToListAsync();
                
        }

        public async Task<TinTuc> GetByIdAsync(int? id)
        {
            return await _context.TINTUC.Include(t => t.ChuDe).Where(b => b.MaTinTuc == id)
                    .FirstOrDefaultAsync();
        }

        public async Task Update(TinTuc entity)
        {
            _context.TINTUC.Update(entity);
            await _context.SaveChangesAsync();
        }
        public Task AddRangeAsync(TinTuc[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
