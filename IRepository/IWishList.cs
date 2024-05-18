using DACS.Models;

namespace DACS.IRepository
{
    public interface IWishList
    {
        public Task<List<Wishlist>> GetWishList(string userId);
        public Task<Wishlist> GetWishlistItemById(int id);
        public Task RemoveProduct(Wishlist item);
        public Task AddAsync(Wishlist item);
        public Task UpdateItem(Wishlist item);
        public Task IncreaseByOne(Wishlist item);


    }
}
