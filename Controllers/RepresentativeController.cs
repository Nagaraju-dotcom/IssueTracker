using IssueTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IssueTrackerProject.Controllers
{
    public class RepresentativeController : Controller
    {
        // GET: Representative
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(CategoryRepresentative representative)
        {
            bool status = false;
            string message = "";

            //Model validation
            if (ModelState.IsValid)
            {
               //saving details to database
                try
                {
                    using (var ctx = new IssueTrackerDBContext())
                    {
                        ctx.CategoryRepresentatives.Add(representative);
                        ctx.SaveChanges();
                        message = "New Representative Created Successfully..!";
                        status = true;
                    }
                }
                catch (Exception)
                {
                    message = "Representative Id already exist";
                }
            }
            else
            {
                message = "Registration was not Successful..!";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(representative);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(RepresentativeLogin login)
        {
            string message = "";
            using (var ctx = new IssueTrackerDBContext())
            {

                var entity = ctx.CategoryRepresentatives.Where(p => p.CategoryRepresentativeId == login.UserId).FirstOrDefault();
                if (entity != null)
                {
                    if (string.Compare(login.Password, entity.Password) == 0)
                    {
                        FormsAuthentication.SetAuthCookie(login.UserId, false);
                        return RedirectToAction("RepresentativeDashboard", "Home");
                    }
                    else
                    {
                        message = "Representative Id or Password is wrong";
                    }
                }
            }
            ViewBag.Messsage = message;
            return View();
        }
        //logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Representative");
        }
    }
}