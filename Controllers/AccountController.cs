using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using EfDbCoreFirst.Identity;
using EfDbCoreFirst.Models;
using EfDbCoreFirst.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace EfDbCoreFirst.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }
        // Post: Account/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if(ModelState.IsValid)
            {
                var appDbContext = new ApplicationDbContext();
                var userStore = new ApplicationUserStore(appDbContext);
                var userManager = new ApplicationUserManager(userStore);
                var passwordHash = Crypto.HashPassword(rvm.Password);
                var user = new ApplicationUser() { Email = rvm.Email, UserName = rvm.Username, PasswordHash = passwordHash, City = rvm.City, Country = rvm.Country, Birthday = rvm.DateOfBirth, Address = rvm.Address, PhoneNumber = rvm.Mobile };
                IdentityResult result = userManager.Create(user);
                if(result.Succeeded)
                {
                    //role

                    userManager.AddToRole(user.Id, "Customer");

                    //login

                    var authenticationManager = HttpContext.GetOwinContext().Authentication; 
                    var userIdentity=userManager.CreateIdentity(user,DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties(),userIdentity);
                 
                }
                return RedirectToAction("index","home");
            }
            else
            {
                ModelState.AddModelError("My Error", "Invalid data");
                return View();
            }
        }
        //get:account/login
        public ActionResult Login()
        {

            return View();
        }
        //post:account/login
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            var appDbContext = new ApplicationDbContext();
            var userStore=new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var user = userManager.Find(lvm.Username, lvm.Password);
            if(user != null)
            {
                //login
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                if (userManager.IsInRole(user.Id,"Admin"))
                {
                    return RedirectToAction("index", "home",new {area="Admin"});
                }
                else
                {
                    return RedirectToAction("index", "home");
                }
               
            }
            else 
            {
                ModelState.AddModelError("My Error", "Invalid Username or Password");
                return View();
            }
        }
        //get:account/logout
        public ActionResult Logout()
        {
            var authenticationManager=HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("index", "home");
        }

        public ActionResult MyProfile()
        {
            var appDbContext = new ApplicationDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            ApplicationUser appUser = userManager.FindById(User.Identity.GetUserId());
            return View(appUser);
        }
    }
}