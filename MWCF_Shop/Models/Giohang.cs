using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWCF_Shop.Models
{
    public class Giohang
    {
        MWC_Shop_UpEntities2 db = new MWC_Shop_UpEntities2();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        // Khởi tạo giỏ hàng theo MaSach được truyền vào với SoLuong mặc định là 1
        public Giohang(int ms)
        {
            iMaSP = ms;
            SANPHAM s = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = s.TenSp;
            sHinh = s.Hinh;
            dDonGia = double.Parse(s.DonGia.ToString());
            iSoLuong = 1;
        }
    }
}