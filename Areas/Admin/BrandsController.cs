using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfDbCoreFirst.Areas.Admin
{
    public class BrandsController : Controller
    {
        // GET: Admin/Brands
        public ActionResult Index()
        {
            return View();
        }
    }
}