using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AprajitaRetails.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Aprajita Retails";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Aprajita Retails";

            return View();
        }
    }
}