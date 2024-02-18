using EfDbCoreFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfDbCoreFirst.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand/index
        public ActionResult Index()
        {
                CompanyDbContext db = new CompanyDbContext();
                List<Brand> brand = db.Brands.ToList();
                return View(brand);
        }
    }
}