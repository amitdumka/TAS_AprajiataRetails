using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TAS_AprajiataRetails.Models.Data;

namespace TAS_AprajiataRetails.Controllers
{
    public class DailySalesController : Controller
    {
        private AprajitaRetailsContext db = new AprajitaRetailsContext();
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
                    DuesList dl = new DuesList() { Amount = dailySale.Amount, DailySale = dailySale };
                    db.DuesLists.Add(dl);
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
        // GET: DailySales
        public ActionResult Index()
        {
            var dailySales = db.DailySales.Include(d => d.Salesman).Where(c => DbFunctions.TruncateTime(c.SaleDate) == DbFunctions.TruncateTime(DateTime.Today)).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            var totalSale = dailySales.Where(c => c.IsManualBill == false).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalManualSale = dailySales.Where(c => c.IsManualBill == true).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalMonthlySale = db.DailySales.Where(c => DbFunctions.TruncateTime(c.SaleDate).Value.Month == DbFunctions.TruncateTime(DateTime.Today).Value.Month).Sum(c => (decimal?)c.Amount) ?? 0;
            var duesamt = db.DuesLists.Where(c => c.IsRecovered == false).Sum(c => (decimal?)c.Amount) ?? 0;
            //var cashinhands = db.CashInHands.Where(c => DbFunctions.TruncateTime(c.CIHDate) == DbFunctions.TruncateTime(DateTime.Today)).FirstOrDefault();
            var cashinhand = db.CashInHands.Where(c => DbFunctions.TruncateTime(c.CIHDate) == DbFunctions.TruncateTime(DateTime.Today)).FirstOrDefault().InHand;

            ViewBag.TodaySale = totalSale;
            ViewBag.ManualSale = totalManualSale;
            ViewBag.MonthlySale = totalMonthlySale;
            ViewBag.DuesAmount = duesamt;
            ViewBag.CashInHand = cashinhand;
           // if (cashinhands != null)
           // ViewBag.CashInHands = (decimal?)(cashinhands.OpenningBalance + cashinhands.CashIn - cashinhands.CashOut) ?? 0;
           //else
           //    ViewBag.CashInHands = 0.00;
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
                ProcessAccounts(dailySale);
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
