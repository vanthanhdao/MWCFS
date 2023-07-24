using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MWCF_Shop.Models;
using WebMatrix.WebData;

namespace MWCF_Shop.Controllers
{
    public class KhachhangController : Controller
    {
        private MWC_Shop_UpEntities2 db = new MWC_Shop_UpEntities2();

        // GET: Khachhang
        public ActionResult Index()
        {
            return View(db.KHACHHANGs.ToList());
        }

        // GET: Khachhang/Details/5
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            // Gan cac gia tri nguoi dung nhap du lieu cho cac bien
            var sTenKH = collection["TenKH"];
            var sTenDN = collection["TenDN"];
            var sMatkhau = collection["MatKhau"];
            var sMatkhauNhapLai = collection["MatKhauNL"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            var sDiachi = collection["DiaChi"];


            if (String.IsNullOrEmpty(sTenKH))
            {
                ViewData["err1"] = "Họ và tên không được rỗng";
            }
            else if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["err2"] = "Tên đăng nhập không được rỗng";
            }
            else if (String.IsNullOrEmpty(sMatkhau))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(sMatkhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMatkhau != sMatkhauNhapLai)
            {
                ViewData["err5"] = "Mật khẩu nhập lại không khớp";
            }
            else if (String.IsNullOrEmpty(sEmail))
            {
                ViewData["err6"] = "Email không được rỗng";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.TenDN == sTenDN) != null)
            {
                ViewBag.ThongBao = "Tên đăng ký đã tồn tại";

            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được sử dụng";
            }
            else
            {
                // Gần giá trị cho đối tượng được tạo mới ( kh )
                kh.TenKH = sTenKH;
                kh.TenDN = Encrypt(sTenDN);
                kh.MatKhau = Encrypt(sMatkhau);
                kh.Email = sEmail;
                kh.DiachiKH = sDiachi;
                kh.DienThoaiKH = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("DangNhap", "Khachhang");
            }
            return this.DangKy();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = Encrypt(collection["TenDN"]);
            var sMatkhau = Encrypt(collection["MatKhau"]);
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
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatkhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }


            }
            return View();
        }

        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }


        public ActionResult DangXuat()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "Home");
        }

        public static string Encrypt(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            encrypt = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());

            }
            return encryptdata.ToString();

        }

 



    }
}
