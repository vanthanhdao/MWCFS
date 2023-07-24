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
    public class ChitietdathangController : BaseController
    {
     

        // GET: Admin/Chitietdathang
        public ActionResult Index(int? id)
        {
            var cTDATHANG = from s in db.CTDATHANGs.Include(c => c.DONDATHANG).Include(c => c.SANPHAM) where s.SoDH==id select s;
            return View(cTDATHANG.ToList());
        }

        // GET: Admin/Chitietdathang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDATHANG cTDATHANG = db.CTDATHANGs.Find(id);
            if (cTDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(cTDATHANG);
        }

        // GET: Admin/Chitietdathang/Create
        public ActionResult Create()
        {
            ViewBag.SoDH = new SelectList(db.DONDATHANGs, "SoDH", "TenNguoiNhan");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp");
            return View();
        }

        // POST: Admin/Chitietdathang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoDH,MaSP,SoLuong,DonGia,ThanhTien")] CTDATHANG cTDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.CTDATHANGs.Add(cTDATHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoDH = new SelectList(db.DONDATHANGs, "SoDH", "TenNguoiNhan", cTDATHANG.SoDH);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", cTDATHANG.MaSP);
            return View(cTDATHANG);
        }

        // GET: Admin/Chitietdathang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDATHANG cTDATHANG = db.CTDATHANGs.Find(id);
            if (cTDATHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.SoDH = new SelectList(db.DONDATHANGs, "SoDH", "TenNguoiNhan", cTDATHANG.SoDH);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", cTDATHANG.MaSP);
            return View(cTDATHANG);
        }

        // POST: Admin/Chitietdathang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoDH,MaSP,SoLuong,DonGia,ThanhTien")] CTDATHANG cTDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTDATHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SoDH = new SelectList(db.DONDATHANGs, "SoDH", "TenNguoiNhan", cTDATHANG.SoDH);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSp", cTDATHANG.MaSP);
            return View(cTDATHANG);
        }

        // GET: Admin/Chitietdathang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDATHANG cTDATHANG = db.CTDATHANGs.Find(id);
            if (cTDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(cTDATHANG);
        }

        // POST: Admin/Chitietdathang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTDATHANG cTDATHANG = db.CTDATHANGs.Find(id);
            db.CTDATHANGs.Remove(cTDATHANG);
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
    }
}
