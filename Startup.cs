using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EfDbCoreFirst.Identity;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(EfDbCoreFirst.Startup))]

namespace EfDbCoreFirst
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions { AuthenticationType=DefaultAuthenticationTypes.ApplicationCookie,LoginPath=new PathString("/Account/Login")});
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            this.CreateRolesAndUsers();
        }
        //create Admin Role
        public void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var appDbContext = new ApplicationDbContext();
            var appUserStore=new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);
            
            
            if(!roleManager.RoleExists("Admin"))
            {
                var role=new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            //create Admin User
            if (userManager.FindByName("admin") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                string userPassword = "admin123";
                var chkuser = userManager.Create(user, userPassword);
                if (chkuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "admin");
                }
            }
            if (!roleManager.RoleExists("Manager"))
            {
                var role=new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }
            if (userManager.FindByName("Manager") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Manager";
                user.Email = "Manager@gmail.com";
                string userPassword = "manager123";
                var checkuser=userManager.Create(user, userPassword);
                if (checkuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Manager");
                }
            }
            if (!roleManager.RoleExists("customer"))
            {
                var role=new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }
        
    }
}
