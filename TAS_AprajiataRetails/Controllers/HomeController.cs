using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Helpers;
using TAS_AprajiataRetails.Models.Views;

namespace TAS_AprajiataRetails.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MasterViewReport reportView = new MasterViewReport();
            reportView.SaleReport = Reports.GetSaleRecord();
            reportView.TailoringReport = Reports.GetTailoringReport();
            return View(reportView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Aprajita Retails Daily Record.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us.";

            return View();
        }
    }
}