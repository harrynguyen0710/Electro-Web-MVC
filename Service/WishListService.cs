using DACS.IRepository;
using DACS.Models;
using DACS.Repository;
using Microsoft.AspNetCore.Identity;

namespace DACS.Service
{
    public class WishListService : IWishListService
    {
        private readonly IWishList _wishList;
        private readonly IProductRepository<SanPham> _productRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUserModel> _userManager;
        public WishListService(IWishList wishList, IProductRepository<SanPham> productRepository
                , IHttpContextAccessor contextAccessor, UserManager<AppUserModel> userManager)
        {
            _wishList = wishList;
            _productRepository = productRepository;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task AddFavoriteProduct(int id)
        {
            var favoriteProduct = await _wishList.GetWishlistItemById(id);
            if (favoriteProduct != null)
            {
                await _wishList.IncreaseByOne(favoriteProduct);
            }
            else
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                Wishlist wishList = new Wishlist()
                {
                    ProductId = id,
                    UserId = user.Id,
                    Quantity = 1
                };
                await _wishList.AddAsync(wishList);
            }
        }
      
        public async Task RemoveFavoriteProduct(int id)
        {
            var favoriteProduct = await _wishList.GetWishlistItemById(id);
            await _wishList.RemoveProduct(favoriteProduct);
        }
        public async Task<Wishlist> GetFavoriteItemById(int id)
        {
            return await _wishList.GetWishlistItemById(id);
        }
        public async Task UpdateWishlistItem(Wishlist item)
        {
            if (item.Quantity == 0)
            {
                await _wishList.RemoveProduct(item);
            }
            else
            {
                await _wishList.UpdateItem(item);
            }
        }
        public async Task BuyProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);          
                var image = product.HinhAnh?.FirstOrDefault()?.FileHinhAnh;

                List<CartItemModel> cart = _contextAccessor.HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                CartItemModel cartItem = cart.FirstOrDefault(c => c.MaSanPham == id);

                if (cartItem == null)
                {
                    cart.Add(new CartItemModel(product, image));
                }
                else
                {
                    cartItem.Soluong += 1;
                }

                _contextAccessor.HttpContext.Session.SetJson("Cart", cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product to cart: {ex.Message}");
                throw;
            }
        }




        public async Task<List<Wishlist>> GetAllFavoriteProducts()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            return await _wishList.GetWishList(user.Id);
        }
    }
}
