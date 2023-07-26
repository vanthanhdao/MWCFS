using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MWCF_Shop.Models;
using PagedList;


namespace MWCF_Shop.Controllers
{
    public class HOMEController : Controller
    {
        private MWC_Shop_UpEntities2 db = new MWC_Shop_UpEntities2();
        public ActionResult Index(int? id,int? page)
        {

            int iSize = 8;
            int iPageNum = (page ?? 1);
            var sp = from s in db.SANPHAMs where s.MaTTSP == 1 select s;
            return View(sp.OrderBy(s => s.MaDM).AsEnumerable().ToPagedList(iPageNum, iSize));
          
        }


        public ActionResult Trang_QA(int? id,int? page)
        {
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var sp = from s in db.SANPHAMs  where s.MaDM == id select s;
            return View(sp.OrderBy(s => s.MaDM).AsEnumerable().ToPagedList(iPageNum, iSize));
        }

        public ActionResult LoaiQA(int? id)
        {
            var qa = from s in db.SANPHAMs where s.MaLoai == id select s;
            return View(qa);
        }

        public ActionResult LoaiQAPartial(int? id)
        {
            var qa = from s in db.LOAISANPHAMs where s.MaDM == id select s;
            ViewBag.QA = qa;
            return PartialView();
        }


       


        public ActionResult Chitietsanpham(int? id, int? id1)
        {

            var ct = from s in db.SANPHAMs where s.MaSP == id select s;
            var lienquan = from t in db.SANPHAMs where t.MaLoai == id1 select t;  
            ViewBag.lq=lienquan.Take(4);
            return View(ct.Single());
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult search(string search = "")
        {

            List<SANPHAM> sp = new List<SANPHAM>();
            if (string.IsNullOrEmpty(search))
            {
                ViewData["errSearchEMpty"] = "Bạn phải nhập từ khóa cần tìm kiếm";
            }
            else
            {
                string input = search;
                // Chúng ta coi các ký tự chữ cái, chữ số và khoảng trắng là "bình thường"
                string pattern = "[^a-zA-Z0-9 ]";
                MatchCollection matches = Regex.Matches(input, pattern);
                if (matches.Count > 0)
                {
                    ViewData["errSearchInvalid"] = "Từ khóa tìm kiếm chứa ký tự không hợp lệ";
                }
                sp = db.SANPHAMs.Where(n => n.TenSp.Contains(search)).ToList();
                return View(sp);
            }
            return View(sp);

            //List<SANPHAM> sp = db.SANPHAMs.Where(n => n.TenSp.Contains(search)).ToList();
            //return View(sp);
        }

        public ActionResult Gioithieu()
        {
            return View();
        }
        public ActionResult Lienhe()
        {
            return View();
        }
        public ActionResult Baiviet()
        {
            return View();
        }
        public ActionResult ChitietBV()
        {
            return View();
        }

        public ActionResult Trang_PK(int? id, int? page)
        {       
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var sp = from s in db.SANPHAMs where s.MaDM == id select s;
            return View(sp.OrderBy(s => s.MaDM).AsEnumerable().ToPagedList(iPageNum, iSize));
        }

        public ActionResult LoaiPK(int? id)
        {
            var qa = from s in db.SANPHAMs where s.MaLoai == id select s;
            return View(qa);
        }

        public ActionResult LoaiPKPartial(int? id)
        {
            var qa = from s in db.LOAISANPHAMs where s.MaDM == id select s;
            ViewBag.QA = qa;
            return PartialView();
        }

        public ActionResult Trang_GD(int? id, int? page)
        {
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var sp = from s in db.SANPHAMs where s.MaDM == id select s;
            return View(sp.OrderBy(s => s.MaDM).AsEnumerable().ToPagedList(iPageNum, iSize));
        }

        public ActionResult LoaiGD(int? id)
        {
            var qa = from s in db.SANPHAMs where s.MaLoai == id select s;
            return View(qa);
        }

        public ActionResult LoaiGDPartial(int? id)
        {
            var qa = from s in db.LOAISANPHAMs where s.MaDM == id select s;
            ViewBag.QA = qa;
            return PartialView();
        }

        //public ActionResult SanphambanchayPartial(int? id,int? id1)
        //{
        //    var sp = from s in db.SANPHAMs where s.MaTTSP == id where s.MaDM == id1 select s;
        //    return PartialView(sp.OrderBy(s => s.MaDM).AsEnumerable());
        //}

        

    }
}
