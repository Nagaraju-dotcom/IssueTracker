
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
        public ActionResult Login(Admin login)
        {
            string message = "";
            using (var issueTrackerContext = new IssueTrackerDBContext())
            {

                var entity = issueTrackerContext.Admins.Where(p => p.AdminId == login.AdminId).FirstOrDefault();
                if (entity != null)
                {
                    if (string.Compare(login.AdminPassword, entity.AdminPassword) == 0)
                    {
                        //SetAuthCookie() sets a browser cookie to initiate the user's session.
                        //It's what keeps the user logged in each time a page is posted to the server.
                        //Learn more about SetAuthCookie
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
            Session.Abandon();//It clears the entire logged in users session
            return RedirectToAction("Index", "Home");
        }
        //Fetching User Issue details  by admin
        public ActionResult ViewUserDetails()
        {
            List<Issue> issueList = new List<Issue>();
            using (var issueTrackerContext = new IssueTrackerDBContext())
            {
                issueList = issueTrackerContext.Issues.ToList();
            }
            return View(issueList);
        }
        //retreving representatives details
        public ActionResult RepresentaiveDetails()
        {
            List<CategoryRepresentative> representatives = new List<CategoryRepresentative>();
            using(var issueTrackerContext = new IssueTrackerDBContext())
            {
                representatives = issueTrackerContext.CategoryRepresentatives.ToList();
            }
            return View(representatives);
        }
        //Deleting  category by admin
        [HttpGet]
        public ActionResult Delete(string id)
        {
            using (var issueTrackerContext = new IssueTrackerDBContext())
            {
            var entity = (from CategoryRepresentatives in issueTrackerContext.CategoryRepresentatives where CategoryRepresentatives.CategoryRepresentativeId==id select CategoryRepresentatives).Single();
            issueTrackerContext.CategoryRepresentatives.Remove(entity);
            issueTrackerContext.SaveChanges();
            return View(entity);
            }
             
        }
        //Updating category by admin
        [HttpGet]
        public ActionResult Update(string id)
        {
            using(var issueTrackerContext=new IssueTrackerDBContext())
            {
                var entity = issueTrackerContext.CategoryRepresentatives.Where(m => m.CategoryRepresentativeId == id).FirstOrDefault();
                CategoryRepresentative categoryRepresentative = new CategoryRepresentative();
                categoryRepresentative.CategoryRepresentativeId = entity.CategoryRepresentativeId;
                categoryRepresentative.FirstName = entity.FirstName;
                categoryRepresentative.LastName = entity.LastName;
                categoryRepresentative.DOB = entity.DOB;
                categoryRepresentative.ContactNumber = entity.ContactNumber;
                return View(categoryRepresentative);
            }
                       
        }
        [HttpPost]
        public ActionResult Update(CategoryRepresentative categoryRepresentative)
        {
            using(var issueTrackerContext=new IssueTrackerDBContext())
            {
                var entity = issueTrackerContext.CategoryRepresentatives.Where(m => m.CategoryRepresentativeId == categoryRepresentative.CategoryRepresentativeId).FirstOrDefault();
                //CategoryRepresentative categoryRepresentative1 = new CategoryRepresentative();
                entity.CategoryRepresentativeId = categoryRepresentative.CategoryRepresentativeId;
                entity.FirstName = categoryRepresentative.FirstName;
                entity.LastName = categoryRepresentative.LastName;
                entity.DOB = categoryRepresentative.DOB;
                entity.ContactNumber = categoryRepresentative.ContactNumber;
                issueTrackerContext.SaveChanges();
                return View(categoryRepresentative);
            }
        }
        //Fetching user's feedback
        public ActionResult ViewFeedback()
        {
            List<Feedback> feedbackList = new List<Feedback>();
            using (var issueTrackerContext = new IssueTrackerDBContext())
            {
                feedbackList = issueTrackerContext.Feedbacks.ToList();
            }
            return View(feedbackList);
        }
        //Generating pdf by using rotativa 
        public ActionResult PrintAllIssues()
        {
            var issuesReport = new Rotativa.ActionAsPdf("ViewUserDetails");
            return issuesReport;
        }
        //Generating pdf by using rotativa 
        public ActionResult PrintFeedback()
        {
            var userFeedback = new Rotativa.ActionAsPdf("ViewFeedback");
            return userFeedback;
        }
    }
}