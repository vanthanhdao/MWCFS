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
    public class QuanlydanhmucController : BaseController
    {
     

        // GET: Admin/Quanlydanhmuc
        public ActionResult Index()
        {
            return View(db.DANHMUC_SP.ToList());
        }

        // GET: Admin/Quanlydanhmuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUC_SP dANHMUC_SP = db.DANHMUC_SP.Find(id);
            if (dANHMUC_SP == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUC_SP);
        }

        // GET: Admin/Quanlydanhmuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Quanlydanhmuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDM,TenDM")] DANHMUC_SP dANHMUC_SP)
        {
            if (ModelState.IsValid)
            {
                db.DANHMUC_SP.Add(dANHMUC_SP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dANHMUC_SP);
        }

        // GET: Admin/Quanlydanhmuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUC_SP dANHMUC_SP = db.DANHMUC_SP.Find(id);
            if (dANHMUC_SP == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUC_SP);
        }

        // POST: Admin/Quanlydanhmuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDM,TenDM")] DANHMUC_SP dANHMUC_SP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANHMUC_SP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dANHMUC_SP);
        }

      
        public ActionResult DeleteConfirmed(int id)
        {
            DANHMUC_SP dANHMUC_SP = db.DANHMUC_SP.Find(id);
            db.DANHMUC_SP.Remove(dANHMUC_SP);
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
