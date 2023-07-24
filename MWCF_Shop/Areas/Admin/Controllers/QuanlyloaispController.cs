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
    public class QuanlyloaispController : BaseController
    {
      
        // GET: Admin/Quanlyloaisp
        public ActionResult Index()
        {
            var lOAISANPHAM = db.LOAISANPHAMs.Include(l => l.DANHMUC_SP);
            return View(lOAISANPHAM.ToList());
        }

        // GET: Admin/Quanlyloaisp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            if (lOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(lOAISANPHAM);
        }

        // GET: Admin/Quanlyloaisp/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP, "MaDM", "TenDM");
            return View();
        }

        // POST: Admin/Quanlyloaisp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoai,TenLoai,MaDM")] LOAISANPHAM lOAISANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.LOAISANPHAMs.Add(lOAISANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDM = new SelectList(db.DANHMUC_SP, "MaDM", "TenDM", lOAISANPHAM.MaDM);
            return View(lOAISANPHAM);
        }

        // GET: Admin/Quanlyloaisp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            if (lOAISANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP, "MaDM", "TenDM", lOAISANPHAM.MaDM);
            return View(lOAISANPHAM);
        }

        // POST: Admin/Quanlyloaisp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoai,TenLoai,MaDM")] LOAISANPHAM lOAISANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAISANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDM = new SelectList(db.DANHMUC_SP, "MaDM", "TenDM", lOAISANPHAM.MaDM);
            return View(lOAISANPHAM);
        }

     
        public ActionResult DeleteConfirmed(int id)
        {
            LOAISANPHAM lOAISANPHAM = db.LOAISANPHAMs.Find(id);
            db.LOAISANPHAMs.Remove(lOAISANPHAM);
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
