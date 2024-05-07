using DACS.Models;

namespace DACS.IRepository
{
    public interface IDonHang : IGenericRepository<DonHang>
    {
        Task<List<DonHang>> GetListDonHangByPhoneNum(string phoneNum);
        decimal GetTotalBill(List<CartItemModel> cartItems);
        decimal GetTotalBillWithVoucher(List<CartItemModel> cartItems, float? tyleGiam); 
      
    }
}
