using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MWCF_Shop.Models;

namespace MWCF_Shop.Areas.Admin.Controllers
{
    public class HomeADController : Controller
    {
        private MWC_Shop_UpEntities2 db = new MWC_Shop_UpEntities2();

        // GET: Admin/HomeAD
        public ActionResult Index()
        {
            var sANPHAM = db.SANPHAMs.Include(s => s.DANHMUC_SP).Include(s => s.LOAISANPHAM);
            return View(sANPHAM.ToList());
        }

        // GET: Admin/HomeAD/Details/5
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

        // GET: Admin/HomeAD/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP, "MaDM", "TenDM");
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoai");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp");
            return View();
        }

        // POST: Admin/HomeAD/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSp,MoTa,DonGia,Hinh,NgayCapNhat,SoluongBan,MaDM,MaLoai")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDM = new SelectList(db.DANHMUC_SP, "MaDM", "TenDM", sANPHAM.MaDM);
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", sANPHAM.MaSP);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", sANPHAM.MaSP);
            return View(sANPHAM);
        }

        // GET: Admin/HomeAD/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP, "MaDM", "TenDM", sANPHAM.MaDM);
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", sANPHAM.MaSP);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", sANPHAM.MaSP);
            return View(sANPHAM);
        }

        // POST: Admin/HomeAD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSp,MoTa,DonGia,Hinh,NgayCapNhat,SoluongBan,MaDM,MaLoai")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP, "MaDM", "TenDM", sANPHAM.MaDM);
            ViewBag.MaLoai = new SelectList(db.LOAISANPHAMs, "MaLoai", "TenLoai", sANPHAM.MaLoai);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", sANPHAM.MaSP);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", sANPHAM.MaSP);
            return View(sANPHAM);
        }

        // GET: Admin/HomeAD/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/HomeAD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatkhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatkhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDNAD == sTenDN && n.MatKhauAD == sMatkhau);
                if (ad != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["Admin"] = ad;
                    return RedirectToAction("Index", "HomeAD");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }


            }
            return View();
        }

        public ActionResult LoginLogoutPartial_AD()
        {
            return PartialView();

        }

        public ActionResult DangXuat()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login","HomeAD");
        }


    }
}
