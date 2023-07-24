using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MWCF_Shop.Models
{
    public class FileModel
    {
        MWC_Shop_UpEntities2 db = new MWC_Shop_UpEntities2();
      
        public HttpPostedFileBase[] files { get; set; }
    }
}