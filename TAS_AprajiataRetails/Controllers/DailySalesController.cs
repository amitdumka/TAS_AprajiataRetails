//using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;
using TAS_AprajiataRetails.Models.Helpers;

namespace TAS_AprajiataRetails.Controllers
{
    [Authorize]
    public class DailySalesController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();
        CultureInfo c = CultureInfo.GetCultureInfo("In");

        /// <summary>
        /// ProcessAccounts Handle Cash/Bank flow and update related tables
        /// </summary>
        /// <param name="dailySale"></param>
        private void ProcessAccounts(DailySale dailySale)
        {
            if (!dailySale.IsSaleReturn)
            {
                if (!dailySale.IsDue)
                {
                    if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                    {
                        Utils.UpDateCashInHand(db, dailySale.SaleDate, dailySale.CashAmount);

                    }
                    //TODO: in future make it more robust
                    if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                    {
                        Utils.UpDateCashInBank(db, dailySale.SaleDate, dailySale.Amount - dailySale.CashAmount);
                    }
                }
                else
                {
                    decimal dueAmt = 0;
                    if (dailySale.Amount != dailySale.CashAmount)
                    {
                        dueAmt = dailySale.Amount - dailySale.CashAmount;
                    }
                    else
                        dueAmt = dailySale.Amount;

                    DuesList dl = new DuesList() { Amount = dueAmt, DailySale = dailySale, DailySaleId = dailySale.DailySaleId };
                    db.DuesLists.Add(dl);

                    if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                    {
                        Utils.UpDateCashInHand(db, dailySale.SaleDate, dailySale.CashAmount);

                    }
                    //TODO: in future make it more robust
                    if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                    {
                        Utils.UpDateCashInBank(db, dailySale.SaleDate, dailySale.Amount - dailySale.CashAmount);
                    }
                }
            }
            else
            {
                if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                {
                    Utils.UpDateCashOutHand(db, dailySale.SaleDate, dailySale.CashAmount);

                }
                //TODO: in future make it more robust
                if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                {
                    Utils.UpDateCashOutBank(db, dailySale.SaleDate, dailySale.Amount - dailySale.CashAmount);
                }
                dailySale.Amount = 0 - dailySale.Amount;

            }

        }

        private void ProcessAccountDelete(DailySale dailySale)
        {
            if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
            {
                Utils.UpDateCashInHand(db, dailySale.SaleDate, 0-dailySale.CashAmount);
            }
            else if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
            {
                Utils.UpDateCashInBank(db, dailySale.SaleDate, 0-dailySale.CashAmount);
            }
            else
            {
                //TODO: Add this option in Create and Edit also
                // Handle when payment is done by Coupons and Points. 
                //Need to create table to create Coupn and Royalty point.
                // Points will go in head for Direct Expenses 
                // Coupon Table will be colloum for TAS Coupon and Apajita Retails. 
                throw new Exception(); 
            }
        }


        // GET: DailySales
        public ActionResult Index(int? id, string salesmanId, string searchString, DateTime? SaleDate)
        {
            var dailySales = db.DailySales.Include(d => d.Salesman).Where(c => DbFunctions.TruncateTime(c.SaleDate) == DbFunctions.TruncateTime(DateTime.Today)).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            if (id != null && id == 101)
            {
                dailySales = db.DailySales.Include(d => d.Salesman).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            }
            //Fixed Query
            var totalSale = dailySales.Where(c => c.IsManualBill == false).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalManualSale = dailySales.Where(c => c.IsManualBill == true).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalMonthlySale = db.DailySales.Where(c => DbFunctions.TruncateTime(c.SaleDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Sum(c => (decimal?)c.Amount) ?? 0;
            var duesamt = db.DuesLists.Where(c => c.IsRecovered == false).Sum(c => (decimal?)c.Amount) ?? 0;

            var cashinhand = (decimal)0.00;
            try
            {
                cashinhand = db.CashInHands.Where(c => DbFunctions.TruncateTime(c.CIHDate) == DbFunctions.TruncateTime(DateTime.Today)).FirstOrDefault().InHand;
            }
            catch (Exception)
            {
                Utils.ProcessOpenningClosingBalance(db, DateTime.Today, false, true);
                cashinhand = (decimal)0.00;
                //Log.Error("Cash In Hand is null");
            }


            // Fixed UI
            ViewBag.TodaySale = totalSale;
            ViewBag.ManualSale = totalManualSale;
            ViewBag.MonthlySale = totalMonthlySale;
            ViewBag.DuesAmount = duesamt;
            ViewBag.CashInHand = cashinhand;

            // By Salesman
            var salesmanList = new List<string>();
            var smQry = from d in db.Salesmen
                        orderby d.SalesmanName
                        select d.SalesmanName;
            salesmanList.AddRange(smQry.Distinct());
            ViewBag.salesmanId = new SelectList(salesmanList);

            //By Date

            var dateList = new List<DateTime>();
            var opdQry = from d in db.DailySales
                         orderby d.SaleDate
                         select d.SaleDate;
            dateList.AddRange(opdQry.Distinct());
            ViewBag.dateID = new SelectList(dateList);

            //By Invoice No Search

            if (!String.IsNullOrEmpty(searchString))
            {
                var dls = db.DailySales.Include(d => d.Salesman).Where(c => c.InvNo == searchString);
                return View(dls);

            }
            else if (!String.IsNullOrEmpty(salesmanId) || SaleDate != null)
            {
                IEnumerable<DailySale> DailySales;

                if (SaleDate != null)
                {
                    DailySales = db.DailySales.Include(d => d.Salesman).Where(c => DbFunctions.TruncateTime(c.SaleDate) == DbFunctions.TruncateTime(SaleDate)).OrderByDescending(c => c.DailySaleId);
                }
                else
                {
                    DailySales = db.DailySales.Include(d => d.Salesman).Where(c => DbFunctions.TruncateTime(c.SaleDate) == DbFunctions.TruncateTime(DateTime.Today)).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
                }

                if (!String.IsNullOrEmpty(salesmanId))
                {
                    DailySales = DailySales.Where(c => c.Salesman.SalesmanName == salesmanId);
                }

                return View(DailySales.ToList());

            }



            return View(dailySales.ToList());
        }

        // GET: DailySales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailySale dailySale = db.DailySales.Find(id);
            if (dailySale == null)
            {
                return HttpNotFound();
            }
            //return View(dailySale);
            return PartialView(dailySale);
        }

        // GET: DailySales/Create
        public ActionResult Create()
        {
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName");
            DailySale dailySale = new DailySale();
            dailySale.SaleDate = DateTime.Today;
            //return View();
            return PartialView(dailySale);
        }

        // POST: DailySales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,IsSaleReturn,Remarks")] DailySale dailySale)
        {
            if (ModelState.IsValid)
            {
                ProcessAccounts(dailySale);
                db.DailySales.Add(dailySale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return View(dailySale);
            //return PartialView(dailySale);
        }

        // GET: DailySales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailySale dailySale = db.DailySales.Find(id);
            if (dailySale == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            //return View(dailySale);
            return PartialView(dailySale);
        }

        // POST: DailySales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,IsSaleReturn,Remarks")] DailySale dailySale)
        {
            if (ModelState.IsValid)
            {
                //TODO: check is required if Amount is changed so need to verify with old data. 
                //TODO: Rectifed This  ProcessAccounts(dailySale);
                db.Entry(dailySale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanName", dailySale.SalesmanId);
            return View(dailySale);
        }

        // GET: DailySales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailySale dailySale = db.DailySales.Find(id);
            if (dailySale == null)
            {

                return HttpNotFound();
            }
            // return View(dailySale);
            return PartialView(dailySale);
        }

        // POST: DailySales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailySale dailySale = db.DailySales.Find(id);
            ProcessAccountDelete(dailySale);
            db.DailySales.Remove(dailySale);
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
