using IssueTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IssueTrackerProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Registration()
        {
            // User u = new User();
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User user)
        {
            bool status = false;
            string message = "";

            //Model validation
            if (ModelState.IsValid)
            {
                //email exist or not
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Emial Id already exist");
                    return View(user);
                }
                //saving details to database
                try
                {
                    using (var ctx = new IssueTrackerDBContext())
                    {
                        ctx.Users.Add(user);
                        ctx.SaveChanges();
                        message = "New User Created Successfully..!";
                        status = true;
                    }
                }
                catch (Exception)
                {
                    message = "User Id already exist";
                }
            }
            else
            {
                message = "Registration was not Successful..!";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }
        //login get
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login)
        {
            string message = "";
            bool status = false;
            using (var ctx = new IssueTrackerDBContext())
            {
                
                var entity = ctx.Users.Where(p => p.UserId == login.UserId).FirstOrDefault();
                if (entity != null)
                {
                    if (string.Compare(login.Password, entity.Password) == 0)
                    {
                        FormsAuthentication.SetAuthCookie(login.UserId, false);
                        message = "New User Created Successfully..!";
                        status = true;
                        return RedirectToAction("Dashboard", "Home");
                        
                    }
                    else
                    {
                        message = "User Id or Password is wrong";
                    }
                }
                else
                {
                    message = "User Id or Password is wrong";
                }
            }
            ViewBag.Messsage = message;
            ViewBag.Status = status;
            return View(login);
        }
        //logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }
        public ActionResult UserDetails()
        {
            var ctx = new IssueTrackerDBContext();
            return View(ctx.Users.ToList());
        }
        [NonAction]
        public bool IsEmailExist(string emailId)
        {
            using (var ctx = new IssueTrackerDBContext())
            {
                var entity = ctx.Users.Where(p => p.Email == emailId).FirstOrDefault();
                return entity != null;
            }
        }
    }
}