using DACS.Data;
using DACS.IRepository;
using DACS.Models;
using DACS.Repository;
using DACS.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUserModel> _userMananger;
        private readonly IWishListService _wishListService;
        private readonly IProductRepository<SanPham> _productRepository;
        
        public WishlistController(ApplicationDbContext context, UserManager<AppUserModel> userManager,
            IWishListService wishListService, IProductRepository<SanPham> productRepository)
        {
            _context = context;
            _userMananger = userManager;
            _wishListService = wishListService;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var favoriteProducts = await _wishListService.GetAllFavoriteProducts();
            return View(favoriteProducts);
        }
        [HttpPost]
        public async Task<IActionResult> BuyProduct(int id)
        {
            await _wishListService.BuyProduct(id);
            await _wishListService.RemoveFavoriteProduct(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWishList(int id)
        {
            await _wishListService.AddFavoriteProduct(id);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int maSanPham, int newQuantity)
        {
            var wishlistItem = await _wishListService.GetFavoriteItemById(maSanPham);
            if (wishlistItem != null)
            {
                wishlistItem.Quantity = newQuantity;
                await _wishListService.UpdateWishlistItem(wishlistItem);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> RemoveProduct(int maSanPham)
        {
            await _wishListService.RemoveFavoriteProduct(maSanPham);
            return Ok();
        }
    }
}
