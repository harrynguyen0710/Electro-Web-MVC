using DACS.Models;
using X.PagedList;
using DACS.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DACS.IRepository
{
    public interface IDonHang : IGenericRepository<DonHang>
    {

        Task<List<DonHang>> GetListDonHangByPhoneNum(string phoneNum, bool sortByDateDescending);

        Task<List<DonHang>> GetListDonHangByPhoneNum(string phoneNum);
        decimal GetTotalBill(List<CartItemModel> cartItems);
        decimal GetTotalBillWithVoucher(List<CartItemModel> cartItems, float? tyleGiam);
/*NOTE HERE*/
        Task<IPagedList<DonHang>> GetPaginatedAsync(int pageIndex, int pageSize);
        IQueryable<DonHang> GetAll();
    }
}
