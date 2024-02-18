using EfDbCoreFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfDbCoreFirst.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Admin/Products
            // GET: Products
            CompanyDbContext db = new CompanyDbContext();
            public ActionResult Index(string search = "", string sortcolumn = "ProductName", string IconClass = "fa-sort-desc", int pageno = 1)
            {
                //CompanyDbContext db = new CompanyDbContext();
                ViewBag.Search = search;
                // List<Product> product=db.Products.Where(temp=>temp.CategoryID==1 && temp.Price>=50000).ToList();
                List<Product> product = db.Products.Where(temp => temp.ProductName.Contains(search)).ToList();
                ViewBag.sortcolumn = sortcolumn;
                ViewBag.IconClass = IconClass;
                if (ViewBag.sortcolumn == "ProductID")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        product = product.OrderBy(temp => temp.ProductID).ToList();
                    }
                    else
                    {
                        product = product.OrderByDescending(temp => temp.ProductID).ToList();
                    }
                }
                else if (ViewBag.sortcolumn == "ProductName")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        product = product.OrderBy(temp => temp.ProductName).ToList();
                    }
                    else
                    {
                        product = product.OrderByDescending(temp => temp.ProductName).ToList();
                    }
                }
                else if (ViewBag.sortcolumn == "Price")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        product = product.OrderBy(temp => temp.Price).ToList();
                    }
                    else
                    {
                        product = product.OrderByDescending(temp => temp.Price).ToList();
                    }
                }
                else if (ViewBag.sortcolumn == "DateOfPurchase")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        product = product.OrderBy(temp => temp.DateOfPurchase).ToList();
                    }
                    else
                    {
                        product = product.OrderByDescending(temp => temp.DateOfPurchase).ToList();
                    }
                }
                else if (ViewBag.sortcolumn == "AvailabilityStatus")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        product = product.OrderBy(temp => temp.AvailabilityStatus).ToList();
                    }
                    else
                    {
                        product = product.OrderByDescending(temp => temp.AvailabilityStatus).ToList();
                    }
                }
                else if (ViewBag.sortcolumn == "CategoryID")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        product = product.OrderBy(temp => temp.Category.CategoryID).ToList();
                    }
                    else
                    {
                        product = product.OrderByDescending(temp => temp.Category.CategoryID).ToList();
                    }
                }
                else if (ViewBag.sortcolumn == "BrandID")
                {
                    if (ViewBag.IconClass == "fa-sort-asc")
                    {
                        product = product.OrderBy(temp => temp.Brand.BrandID).ToList();
                    }
                    else
                    {
                        product = product.OrderByDescending(temp => temp.Brand.BrandID).ToList();
                    }
                }
                int noofrecordperpage = 5;
                int noofpage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(product.Count) / Convert.ToDouble(noofrecordperpage)));
                int noofrecordstoskip = (pageno - 1) * noofrecordperpage;
                ViewBag.pageno = pageno;
                ViewBag.noofpage = noofpage;
                product = product.Skip(noofrecordstoskip).Take(noofrecordperpage).ToList();
                return View(product);
            }

            public ActionResult Details(long id)
            {
                //CompanyDbContext db = new CompanyDbContext();
                Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
                return View(p);
            }
            public ActionResult Create()
            {
                //CompanyDbContext db = new CompanyDbContext();
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Brands = db.Brands.ToList();
                return View();
            }
            [HttpPost]
            public ActionResult Create([Bind(Include = "ProductID,ProductName,Price,DateOfPurchase,AvailabilityStatus,CategoryID,BrandID,Active,Photo")] Product p)
            {
                if (ModelState.IsValid)
                {
                    //CompanyDbContext db = new CompanyDbContext();
                    if (Request.Files.Count >= 1)
                    {
                        var file = Request.Files[0];
                        var imgbytes = new Byte[file.ContentLength];
                        file.InputStream.Read(imgbytes, 0, file.ContentLength);
                        var base64String = Convert.ToBase64String(imgbytes, 0, imgbytes.Length);
                        p.Photo = base64String;
                    }
                    db.Products.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Categories = db.Categories.ToList();
                    ViewBag.Brands = db.Brands.ToList();
                    return View();
                }

            }
            public ActionResult Edit(long id)
            {
                // CompanyDbContext db = new CompanyDbContext(); 
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Brands = db.Brands.ToList();
                Product existing = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
                return View(existing);
            }
            [HttpPost]
            public ActionResult Edit(Product p)
            {
                //CompanyDbContext db = new CompanyDbContext();
                Product existing = db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();
                existing.ProductName = p.ProductName;
                existing.Price = p.Price;
                existing.DateOfPurchase = p.DateOfPurchase;
                existing.AvailabilityStatus = p.AvailabilityStatus;
                existing.CategoryID = p.CategoryID;
                existing.BrandID = p.BrandID;
                existing.Active = p.Active;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            public ActionResult Delete(long id)
            {
                //CompanyDbContext db = new CompanyDbContext();
                Product existing = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
                return View(existing);
            }
            [HttpPost]
            public ActionResult Delete(long id, Product p)
            {
                //CompanyDbContext db = new CompanyDbContext();
                Product existing = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
                db.Products.Remove(existing);
                db.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
        }
    }