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
    public class QuanlydonhangController : BaseController
    {
       

        // GET: Admin/Quanlydonhang
        public ActionResult Index()
        {
            var dONDATHANG = db.DONDATHANGs.Include(d => d.KHACHHANG);
            return View(dONDATHANG.ToList());
        }

        // GET: Admin/Quanlydonhang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = db.DONDATHANGs.Find(id);
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONDATHANG);
        }

        // GET: Admin/Quanlydonhang/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH");
            return View();
        }

        // POST: Admin/Quanlydonhang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoDH,MaKH,NgayDH,TriGia,NgayGiaoHang,TenNguoiNhan,DiaChiNhan,DienThoaiNhan,HTThanhToan,HTGiaoHang")] DONDATHANG dONDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.DONDATHANGs.Add(dONDATHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH", dONDATHANG.MaKH);
            return View(dONDATHANG);
        }

        // GET: Admin/Quanlydonhang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = db.DONDATHANGs.Find(id);
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH", dONDATHANG.MaKH);
            return View(dONDATHANG);
        }

        // POST: Admin/Quanlydonhang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoDH,MaKH,NgayDH,TriGia,NgayGiaoHang,TenNguoiNhan,DiaChiNhan,DienThoaiNhan,HTThanhToan,HTGiaoHang")] DONDATHANG dONDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONDATHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH", dONDATHANG.MaKH);
            return View(dONDATHANG);
        }

        // GET: Admin/Quanlydonhang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = db.DONDATHANGs.Find(id);
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONDATHANG);
        }

        // POST: Admin/Quanlydonhang/Delete/5
     
        public ActionResult DeleteConfirmed(int id)
        {
            DONDATHANG dONDATHANG = db.DONDATHANGs.Find(id);
            db.DONDATHANGs.Remove(dONDATHANG);
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
