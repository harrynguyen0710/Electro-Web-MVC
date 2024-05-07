namespace DACS.Models
{
    public class CartItemModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; } 
        public string HinhAnh { get; set; }
        public int Soluong { get; set; }
        public decimal Gia { get; set; }
        public decimal? GiaKhuyenMai { get; set; }  
        public decimal TongTien {
            get
            {
                if (GiaKhuyenMai == null)
                {
                    return Soluong * Gia;
                }
                else
                {
                    return (decimal)(Soluong * GiaKhuyenMai);
                }
            }
        }
        public decimal TongSoLuong
        {
            get { return Soluong; }
        }
        public List<SanPham> SanPhamList { get; set; }
        public List<HinhAnh> HinhAnhList { get; set; }



        public CartItemModel() { }
        public CartItemModel(SanPham? sanPham) { }//neu khong bam vao gio hang thi gio hang se trong
        public CartItemModel(SanPham sanPham, string hinhAnh)
        {
            MaSanPham = sanPham.MaSanPham;
            TenSanPham = sanPham.TenSanPham;
            Gia = sanPham.Gia;
            Soluong = 1;
            HinhAnh = hinhAnh;
            GiaKhuyenMai = sanPham.GiaKhuyenMai;
        }
    }
}
