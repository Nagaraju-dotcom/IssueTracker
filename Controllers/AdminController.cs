using IssueTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IssueTrackerProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin login)
        {
            string message = "";
            using (var ctx = new IssueTrackerDBContext())
            {

                var entity = ctx.Admins.Where(p => p.AdminId == login.AdminId).FirstOrDefault();
                if (entity != null)
                {
                    if (string.Compare(login.AdminPassword, entity.AdminPassword) == 0)
                    {
                        FormsAuthentication.SetAuthCookie(login.AdminId, false);
                        return RedirectToAction("AdminDashboard", "Home");
                    }
                    else
                    {
                        message = "Admin Id or Password is wrong";
                    }
                }
            }
            ViewBag.Messsage = message;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
        
        public ActionResult ViewUserDetails()
        {
            List<User> userList = new List<User>();
            using (var ctx=new IssueTrackerDBContext())
            {
                userList=ctx.Users.ToList();
            }
            return View("ViewUserDetails");
        }
    }
}