using DACS.Models;

namespace DACS.IRepository
{
    public interface IDonHang : IGenericRepository<DonHang>
    {
        Task<List<DonHang>> GetListDonHangByPhoneNum(string phoneNum);
    }
}
