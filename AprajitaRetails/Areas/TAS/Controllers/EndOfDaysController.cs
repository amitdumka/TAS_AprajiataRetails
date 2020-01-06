using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AprajitaRetails.Areas.TAS.Models.Data;
using AprajitaRetails.Areas.TAS.Models.Helpers;
using AprajitaRetails.Areas.TAS.Models.Views;

namespace AprajitaRetails.Areas.TAS.Controllers
{
    [Authorize]
    public class EndOfDaysController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();

        private EndofDayDetails ProcessEndOfDay(EndOfDay day)
        {

            // Process opening & closing balance of day.
            // Utils.ProcessOpenningClosingBalance(db, day.EOD_Date, true);
            // Utils.ProcessOpenningClosingBankBalance(db, day.EOD_Date, true); //TODO: Check in future

            //Create Next day openning Balance
            Utils.CreateNextDayOpenningBalance(db, day.EOD_Date, true);
            // process for a report to be sms

            var todaySale = db.DailySales.Where(c => DbFunctions.TruncateTime(c.SaleDate) == DbFunctions.TruncateTime(day.EOD_Date));
            decimal totalExp = 0;
            decimal tRec = 0;
            decimal tPay = 0;
            totalExp = (decimal?)db.Expenses.Where(c => DbFunctions.TruncateTime(c.ExpDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
            totalExp += (decimal?)db.CashExpenses.Where(c => DbFunctions.TruncateTime(c.ExpDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;

            tPay = (decimal?)db.Payments.Where(c => DbFunctions.TruncateTime(c.PayDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
            tPay += (decimal?)db.CashPayments.Where(c => DbFunctions.TruncateTime(c.PaymentDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
            tPay += (decimal?)db.Salaries.Where(c => DbFunctions.TruncateTime(c.PaymentDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
            tPay += (decimal?)db.StaffAdvancePayments.Where(c => DbFunctions.TruncateTime(c.PaymentDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;


            tRec = (decimal?)db.Receipts.Where(c => DbFunctions.TruncateTime(c.RecieptDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
            tPay += (decimal?)db.CashReceipts.Where(c => DbFunctions.TruncateTime(c.InwardDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
            tPay += (decimal?)db.Withdrawals.Where(c => DbFunctions.TruncateTime(c.DepoDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;
            tPay += (decimal?)db.StaffAdvanceReceipts.Where(c => DbFunctions.TruncateTime(c.ReceiptDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (decimal?)c.Amount) ?? 0;


            EndofDayDetails details = new EndofDayDetails()
            {

                TodaySale = (decimal?)todaySale.Sum(c => (decimal?)c.Amount) ?? 0,
                TodayCardSale = (decimal?)todaySale.Where(c => c.PayMode == PayModes.Card).Sum(c => (decimal?)c.Amount) ?? 0,
                TodayManualSale = (decimal?)todaySale.Where(c => c.IsManualBill == true).Sum(c => (decimal?)c.Amount) ?? 0,
                TodayOtherSale = (decimal?)todaySale.Where(c => c.PayMode != PayModes.Card && c.PayMode != PayModes.Cash).Sum(c => (decimal?)c.Amount) ?? 0,
                TodayTailoringSale = (decimal?)todaySale.Where(c => c.IsTailoringBill == true).Sum(c => (decimal?)c.Amount) ?? 0,
                TodayCashInHand = (decimal?)db.CashInHands.Where(c => DbFunctions.TruncateTime(c.CIHDate) == DbFunctions.TruncateTime(day.EOD_Date)).FirstOrDefault().InHand ?? 0,
                TodayTailoringBooking = (int?)db.Bookings.Where(c => DbFunctions.TruncateTime(c.BookingDate) == DbFunctions.TruncateTime(day.EOD_Date)).Count() ?? 0,
                TodayTotalUnit = (int?)db.Bookings.Where(c => DbFunctions.TruncateTime(c.BookingDate) == DbFunctions.TruncateTime(day.EOD_Date)).Sum(c => (int?)c.TotalQty) ?? 0,
                TodayTotalExpenses = totalExp,
                TotalPayments = tPay,
                TotalReceipts = tRec,
                Access = day.Access,
                CashInHand = day.CashInHand,
                EOD_Date = day.EOD_Date,
                FM_Arrow = day.FM_Arrow,
                RWT = day.RWT,
                Shirting = day.Shirting,
                Suiting = day.Suiting,
                USPA = day.USPA

            };



            return details;



        }

        public ActionResult DailySaleReport(EndofDayDetails details)
        {
            //details = new EndofDayDetails();
            return View(details);
        }

        // GET: EndOfDays
        public ActionResult Index()
        {
            return View(db.EndOfDays.ToList());
        }

        // GET: EndOfDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EndOfDay endOfDay = db.EndOfDays.Find(id);
            if (endOfDay == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("DailySaleReport", ProcessEndOfDay(endOfDay));
            //return View(endOfDay);
        }

        // GET: EndOfDays/Create
        public ActionResult Create()
        {
            EndOfDay day = new EndOfDay
            {
                EOD_Date = DateTime.Today
            };
            return View(day);
        }

        // POST: EndOfDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EndOfDayId,EOD_Date,Shirting,Suiting,USPA,FM_Arrow,RWT,Access,CashInHand")] EndOfDay endOfDay)
        {
            if (ModelState.IsValid)
            {
                db.EndOfDays.Add(endOfDay);
                db.SaveChanges();
               // EndofDayDetails eod = ProcessEndOfDay(endOfDay);

                return RedirectToAction("DailySaleReport", ProcessEndOfDay(endOfDay));
            }

            return View(endOfDay);
        }

        // GET: EndOfDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EndOfDay endOfDay = db.EndOfDays.Find(id);
            if (endOfDay == null)
            {
                return HttpNotFound();
            }
            return View(endOfDay);
        }

        // POST: EndOfDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EndOfDayId,EOD_Date,Shirting,Suiting,USPA,FM_Arrow,RWT,Access,CashInHand")] EndOfDay endOfDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endOfDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(endOfDay);
        }

        // GET: EndOfDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EndOfDay endOfDay = db.EndOfDays.Find(id);
            if (endOfDay == null)
            {
                return HttpNotFound();
            }
            return View(endOfDay);
        }

        // POST: EndOfDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EndOfDay endOfDay = db.EndOfDays.Find(id);
            db.EndOfDays.Remove(endOfDay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
