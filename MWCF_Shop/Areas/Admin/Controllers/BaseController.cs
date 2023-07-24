using Microsoft.Ajax.Utilities;
using MWCF_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MWCF_Shop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
       
        public MWC_Shop_UpEntities2 db = new MWC_Shop_UpEntities2();
        public Member member;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Admin"] == null)
            {

                filterContext.Result = new RedirectResult("/Admin/HomeAD/Login");
            }
          
            base.OnActionExecuting(filterContext);
        }

      







    }
}