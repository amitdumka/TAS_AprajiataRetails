using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;
using TAS_AprajiataRetails.Models.Helpers;
using TAS_AprajiataRetails.Models.Views;

namespace TAS_AprajiataRetails.Controllers
{
    public class IncomeExpenseReportController : Controller
    {
        private AprajitaRetailsContext _context = new AprajitaRetailsContext();

        public ActionResult IEReport()
        {
            IEReport dM = new IEReport();
            IERVM data = new IERVM
            {

                CurrentWeek = dM.GetWeeklyReport(_context),
                Monthly = dM.GetMonthlyReport(_context, DateTime.Today),
                Today = dM.GetDailyReport(_context, DateTime.Today),
                Yearly = dM.GetYearlyReport(_context, DateTime.Today)
            };

            return View(data);
        }

        // GET: IncomeExpenseReport
        public ActionResult Index(int? id)
        {
            IncomeExpensesReport ierData = null;
            IEReport dM = new IEReport();
            if (id == 1)
            {

                ierData = dM.GetDailyReport(_context, DateTime.Today);
            }
            else if (id == 7)
            {
                ierData = dM.GetWeeklyReport(_context);
            }
            else if (id == 30)
            {
                ierData = dM.GetMonthlyReport(_context, DateTime.Today);
            }
            else if (id == 365)
            {
                ierData = dM.GetYearlyReport(_context, DateTime.Today);
            }
            else
            {
                ierData = dM.GetDailyReport(_context, DateTime.Today);
            }

            return View(ierData);
            
        }
    }
}