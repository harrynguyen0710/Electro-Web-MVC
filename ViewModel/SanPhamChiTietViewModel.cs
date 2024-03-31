using DACS.Data;
using DACS.Models;
namespace DACS.ViewModel

{
    public class SanPhamChiTietViewModel
    {

        public SanPham SanPham { get; set; }
        public Iphone Iphone { get; set; }
        public Ipad Ipad { get; set; }
        public Laptop Laptop { get; set; }
        public IMac Imac { get; set; }

        public ThuongHieu ThuongHieu { get; set; }
        public Ram Ram { get; set; }
        public MauSac MauSac { get; set; }
        public BoNho BoNho { get; set; }

        public BinhLuan BinhLuan { get; set; }


/*        public List<AppUserModel> User { get; set; }
*/
        public AppUserModel User { get; set; }
        public List<BinhLuan> BinhLuanList { get; set; }


        public List<Iphone> IphoneList { get; set; }  
        public List<Ipad> IpadList { get; set; }
        public List<IMac> IMacList { get; set; }
        public List<Laptop> LaptopList { get; set; }
        public List<SanPham> SanPhamList { get; set; }
        public List<HinhAnh> HinhAnhList { get; set; }
        public List<HinhAnhQuangCao> HinhAnhQuangCao { get; set; }  

        /*Context*/
        public ApplicationDbContext context { get; set; }
    }
}