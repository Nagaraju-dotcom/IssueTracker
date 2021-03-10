using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueTrackerProject.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }
        [Authorize]
        public ActionResult RepresentativeDashboard()
        {
            return View();
        }
        [Authorize]
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}