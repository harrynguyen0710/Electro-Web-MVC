using DACS.Models;

namespace DACS.Service
{
    public interface IWishListService
    {
        public Task AddFavoriteProduct(int id);
        public Task<Wishlist> GetFavoriteItemById(int id);
        public Task UpdateWishlistItem(Wishlist item);
        public Task RemoveFavoriteProduct(int id);
        public Task BuyProduct(int id);
        public Task<List<Wishlist>> GetAllFavoriteProducts();

    }
}
