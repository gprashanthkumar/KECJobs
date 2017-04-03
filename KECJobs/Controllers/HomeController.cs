using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KECJobs.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About \"" + "KEC Jobs Portal\"" +".";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NotAuthorised()
        {
            ViewBag.Message = "You do not have authorisation to perform this action on this page.";
            return View();
        }
    }
}