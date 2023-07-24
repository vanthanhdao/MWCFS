using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using MWCF_Shop.Models;
using PagedList;

namespace MWCF_Shop.Areas.Admin.Controllers
{
    public class QuanlysanphamController : BaseController
    {
       
        // GET: Admin/Quanlysanpham

        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
        }




        // GET: Admin/Quanlysanpham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }


        public ActionResult DeleteConfirmed(int? id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaTTSP = new SelectList(db.TRANGTHAISPs.ToList().OrderBy(n => n.TenTTSP), "MaTTSP", "TenTTSP");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(SANPHAM sach, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            // Đưa dữ liệu vào DropDown
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaTTSP = new SelectList(db.TRANGTHAISPs.ToList().OrderBy(n => n.TenTTSP), "MaTTSP", "TenTTSP");
            if (fFileUpload == null)
            {
                // Nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = " Hãy chọn ảnh bìa . ";
                // Lưu thông tin để khi load lại trang do yêu cầu chọn ảnh bìa sẽ hiển thị               các thông tin này lên trang
                ViewBag.TenSp = f["sTenSp"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.DonGia = decimal.Parse(f["mGiaBan"]);

                ViewBag.MaDM = new SelectList(db.DANHMUC_SP.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM", int.Parse(f["MaDM"]));
                ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", int.Parse(f["MaLoai"]));
                ViewBag.MaTTSP = new SelectList(db.TRANGTHAISPs.ToList().OrderBy(n => n.TenTTSP), "MaTTSP", "TenTTSP", int.Parse(f["MaTTSP"]));
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {

                    var sFileName = Path.GetFileName(fFileUpload.FileName);

                    var path = Path.Combine(Server.MapPath("~/assets/img/gallery/"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    // Lưu Sạch vào CSDL
                    sach.TenSp = f["sTenSp"];
                    sach.MoTa = f["sMoTa"].Replace("<p>", " ").Replace("</p> ", "\n");
                    sach.Hinh = sFileName;
                    sach.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sach.SoluongBan = int.Parse(f["iSoLuong"]);
                    sach.DonGia = decimal.Parse(f["mGiaBan"]);
                    sach.MaDM = int.Parse(f["MaDM"]);
                    sach.MaLoai = int.Parse(f["MaLoai"]);
                    //sach.MaTTSP = int.Parse(f["MaTTSP"]);
                    db.SANPHAMs.Add(sach);
                    db.SaveChanges();
                    // Về lại trang Quản lý sách
                    return RedirectToAction("Index");
                }
                return View();
            }
        }




        [HttpGet]
        public ActionResult Update(int? id)
        {
            var sach = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaTTSP = new SelectList(db.TRANGTHAISPs.ToList().OrderBy(n => n.TenTTSP), "MaTTSP", "TenTTSP");
            return View(sach);


        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            //try
            //{
            var sach = db.SANPHAMs.AsEnumerable().SingleOrDefault(n => n.MaSP == Convert.ToInt32(f["iMaSP"]));
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaTTSP = new SelectList(db.TRANGTHAISPs.ToList().OrderBy(n => n.TenTTSP), "MaTTSP", "TenTTSP");
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/img/gallery"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sach.Hinh = sFileName;
                }
                sach.TenSp = f["sTenSp"];
                sach.MoTa = f["sMoTa"].Replace("<p>", " ").Replace("</p> ", "\n");

                sach.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                sach.SoluongBan = int.Parse(f["iSoLuong"]);
                sach.DonGia = decimal.Parse(f["mGiaBan"]);
                sach.MaDM = int.Parse(f["MaDM"]);
                sach.MaLoai = int.Parse(f["Maloai"]);
                sach.MaTTSP = int.Parse(f["MaTTSP"]);

                db.SaveChanges();
                // Về lại trang Quản lý sách
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult searchAD(int? page, string search = " ")
        {
            List<SANPHAM> sp = db.SANPHAMs.Where(n => n.TenSp.Contains(search)).ToList();
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(sp.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
        }

       



    }
}
