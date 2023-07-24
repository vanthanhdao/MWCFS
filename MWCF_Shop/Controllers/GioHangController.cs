using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MWCF_Shop.Models;





namespace MWCF_Shop.Controllers
{
    public class GioHangController : Controller
    {
        private MWC_Shop_UpEntities2 db = new MWC_Shop_UpEntities2();

       
        

        // GET: GIOHANG
        public ActionResult Index()
        {
            var sANPHAM = db.SANPHAMs.Include(s => s.DANHMUC_SP).Include(s => s.LOAISANPHAM);
            return View(sANPHAM.ToList());
        }

        // GET: GIOHANG/Details/5

        public List<Giohang> LayGiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;

            }
            return lstGiohang;
        }

        public ActionResult ThemGiohang(int ms, string url)
        {
            List<Giohang> lstGiohang = LayGiohang();
            Giohang sp = lstGiohang.Find(n => n.iMaSP == ms);
            if (sp == null)
            {
                sp = new Giohang(ms);
                lstGiohang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }

       
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoLuong);

            }
            return iTongSoLuong;
        }
        static double dTongTien = 0;
        public double getTongTien()
        {
            return dTongTien;
        }
        public double TongTien()
        {
           
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                dTongTien = lstGiohang.Sum(n => n.dThanhTien);

            }
            return dTongTien;
        }

        public ActionResult Giohang()
        {
            List<Giohang> lstGiohang = LayGiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGiohang);


        }

        public ActionResult GiohangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult XoaSP(int iMaSP)
        {
            List<Giohang> lstGiohang = LayGiohang();
            Giohang sp = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);

            if (sp != null)
            {
                lstGiohang.RemoveAll(n => n.iMaSP == iMaSP);
                if (lstGiohang.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult CapNhatGiohang(int iMaSP, FormCollection f)
        {
            List<Giohang> lstGiohang = LayGiohang();
            Giohang sp = lstGiohang.SingleOrDefault(n => n.iMaSP == iMaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("Giohang");

        }


        public ActionResult XoaGiohang()
        {

            List<Giohang> lstGiohang = LayGiohang();
            lstGiohang.Clear();

            return RedirectToAction("Index", "Home");


        }


        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng nhập chưa
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Khachhang");
            }
            //Kiểm tra có hàng trong giỏ
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //lẤY HÀNG tỪ SESSION
            List<Giohang> lstGiohang = LayGiohang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGiohang);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            //Thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<Giohang> lstGiohang = LayGiohang();
            ViewBag.TongTien = TongTien();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDH = DateTime.Now;
            ddh.TenNguoiNhan = kh.TenKH;
            ddh.DiaChiNhan = kh.DiachiKH;
            ddh.DienThoaiNhan = kh.DienThoaiKH;
            ddh.TriGia = Convert.ToDecimal(ViewBag.TongTien);
            
            //var NgayGiaoHang = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            //ddh.NgayGiaoHang = DateTime.Parse(NgayGiaoHang);
            //ddh.HTGiaoHang = true;
            //ddh.HTThanhToan = false;
            db.DONDATHANGs.Add(ddh);
            db.SaveChanges();
            //Thêm ct đơn hàng
            foreach (var item in lstGiohang)
            {
                CTDATHANG ctdh = new CTDATHANG();
                ctdh.SoDH = ddh.SoDH;
                ctdh.MaSP = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;
               db.CTDATHANGs.Add(ctdh);
            }
            db.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XacNhanDatHang", "Giohang");


        }

        public ActionResult XacNhanDatHang()
        {
            
            return View();
        }


        


    }
}
