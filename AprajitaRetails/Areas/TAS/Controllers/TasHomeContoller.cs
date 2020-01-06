using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AprajitaRetails.Areas.TAS.Models.Helpers;
using AprajitaRetails.Areas.TAS.Models.Views;

namespace AprajitaRetails.Areas.TAS.Controllers
{
    public class HomeContoller : Controller
    {
        public ActionResult Index()
        {
            MasterViewReport reportView = new MasterViewReport
            {
                SaleReport = Reports.GetSaleRecord(),
                TailoringReport = Reports.GetTailoringReport(),
                EmpInfoList = Reports.GetEmpInfo(),
                AccountsInfo = Reports.GetAccoutingRecord()
            };
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