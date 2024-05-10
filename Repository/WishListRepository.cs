using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace DACS.Repository
{
    public class WishListRepository : IWishList
    {
        private readonly ApplicationDbContext _context;

        public WishListRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Wishlist>> GetWishList(string userId)
        {
            return await _context.WISHLIST.Where(wl => wl.UserId == userId)
                .Include(pr => pr.SanPham)
                    .ThenInclude(img => img.HinhAnh)
                .ToListAsync();
        }
        public async Task AddAsync(Wishlist wishList)
        {
            await _context.WISHLIST.AddAsync(wishList);
            await _context.SaveChangesAsync();
        }
        public async Task<Wishlist> GetWishlistItemById(int id)
        {
            var favoriteProduct = await _context.WISHLIST.Where(item => item.ProductId == id).FirstOrDefaultAsync();
            return favoriteProduct;
        }
        public async Task IncreaseByOne(Wishlist item)
        {
            item.Quantity += 1;
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateItem(Wishlist item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveProduct(Wishlist item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }


    }
}
